using AutoMapper;
using MediatR;
using System.Text.RegularExpressions;
using wcc.gateway.data;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using Core = wcc.gateway.kernel.Models.Core;
using Microservices = wcc.gateway.kernel.Models.Microservices;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class MigratePlayerQuery : IRequest<bool>
    {
        public UserModelOld User { get; }

        public MigratePlayerQuery(UserModelOld user)
        {
            User = user;
        }
    }

    public class MigrateTeamsQuery : IRequest<bool>
    {
        public MigrateTeamsQuery()
        {
        }
    }

    public class MigrateTournamentsQuery : IRequest<bool>
    {
        public MigrateTournamentsQuery()
        {
        }
    }

    public class MigrateGamesQuery : IRequest<bool>
    {
        public MigrateGamesQuery()
        {
        }
    }

    public class MigrationHandler : IRequestHandler<MigratePlayerQuery, bool>,
        IRequestHandler<MigrateTeamsQuery, bool>,
        IRequestHandler<MigrateTournamentsQuery, bool>,
        IRequestHandler<MigrateGamesQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly Microservices.Config _mcsvcConfig;

        public MigrationHandler(IDataRepository db, Microservices.Config mcsvcConfig)
        {
            _db = db;
            _mcsvcConfig = mcsvcConfig;
        }

        public async Task<bool> Handle(MigratePlayerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var players = _db.GetPlayers();

                foreach (var player in players)
                {
                    var token = string.IsNullOrEmpty(player.Token) ? CommonHelper.GenerateToken() : player.Token;

                    var result = await new ApiCaller(_mcsvcConfig.CoreUrl).PostAsync<Core.PlayerModel, bool>("api/player",
                        new Core.PlayerModel
                        {
                            Name = player.Name,
                            UserId = player.UserId.ToString(),
                            IsActive = true,
                            Token = token
                        });

                    if (!result) return false;

                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {

            }

            return true;
        }

        public async Task<bool> Handle(MigrateTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = _db.GetTeams();
            if (teams == null) return false;

            var playerData = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<Core.PlayerModel>>("api/player");

            Thread.Sleep(1000);

            foreach (var team in teams)
            {
                var userIds = team.Players.Select(u => u.UserId.ToString()).ToList();

                var players = playerData.Where(p => p.UserId != null && userIds.Contains(p.UserId)).Select(p => p.Id).ToList();

                if (players == null) return false;

                var result = await new ApiCaller(_mcsvcConfig.CoreUrl).PostAsync<Core.TeamModel, bool>("api/team",
                    new Core.TeamModel
                    {
                        Name = team.Id + " - " + team.Name,
                        PlayerIds = players,
                        TournamentId = team.TournamentId
                    });

                if (!result) return false;

                Thread.Sleep(1500);
            }

            return true;
        }

        public async Task<bool> Handle(MigrateTournamentsQuery request, CancellationToken cancellationToken)
        {
            var tournaments = _db.GetTournaments();
            if (tournaments == null) return false;

            foreach (var tournament in tournaments)
            {
                var type = _db.GetGames().FirstOrDefault(g => g.TournamentId == tournament.Id)?.GameType;
                if (!type.HasValue) return false;

                Thread.Sleep(1000);

                var description = tournament.Translations.First(t => t.LanguageId == 1);

                var result = await new ApiCaller(_mcsvcConfig.CoreUrl).PostAsync<Core.TournamentModel, bool>("api/tournament",
                    new Core.TournamentModel
                    {
                        Name = tournament.Id + " - " + description.Name,
                        Description = description.Description,
                        ImageUrl = tournament.ImageUrl,
                        GameType = (GameType)type
                    });

                if (!result) return false;

                Thread.Sleep(1000);
            }

            return true;
        }

        public async Task<bool> Handle(MigrateGamesQuery request, CancellationToken cancellationToken)
        {
            var games = _db.GetGames();
            if (games == null) return false;

            var corePlayers = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<Core.PlayerModel>>("api/player");
            Thread.Sleep(1000);

            var coreTeams = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<Core.TeamModel>>("api/team");
            Thread.Sleep(1000);

            var coreTournaments = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<Core.TournamentModel>>("api/tournament");
            Thread.Sleep(1000);

            foreach (var game in games)
            {
                var type = game.GameType;
                
                List<string> sideA = new List<string>();
                List<string> sideB = new List<string>();

                if (type == GameType.Individual)
                {
                    sideA.AddRange(corePlayers.Where(p => p.UserId == game.HUserId.ToString()).Select(p => p.Id).ToList());
                    sideB.AddRange(corePlayers.Where(p => p.UserId == game.VUserId.ToString()).Select(p => p.Id).ToList());
                }
                else if (type == GameType.Teams)
                {
                    sideA.AddRange(coreTeams.Where(p => p.Name.StartsWith(game.HUserId.ToString() + " - ")).Select(p => p.Id).ToList());
                    sideB.AddRange(coreTeams.Where(p => p.Name.StartsWith(game.VUserId.ToString() + " - ")).Select(p => p.Id).ToList());
                }
                else
                {
                    return false;
                }

                var tournamentId = coreTournaments.FirstOrDefault(t => t.Name.StartsWith(game.TournamentId.ToString() + " - "))?.Id;

                var youtube = new List<string>();
                if (game.YoutubeUrls != null && game.YoutubeUrls.Any())
                {
                    foreach(var url in game.YoutubeUrls)
                    {
                        youtube.Add(url.Url ?? string.Empty);
                    }
                }

                var result = await new ApiCaller(_mcsvcConfig.CoreUrl).PostAsync<Core.GameModel, bool>("api/game",
                    new Core.GameModel
                    {
                        GameType = type,
                        SideA = sideA,
                        SideB = sideB,
                        ScoreA = game.HScore,
                        ScoreB = game.VScore,
                        TournamentId = tournamentId,
                        Scheduled = game.Scheduled.ToUniversalTime(),
                        Youtube = youtube,
                    });

                if (!result)
                {
                    return false;
                }

                Thread.Sleep(1000);
            }

            // update teams names
            foreach (var coreTeam in coreTeams)
            {
                string pattern = "\\d* - ";
                string newName = Regex.Replace(coreTeam.Name, pattern, string.Empty);

                var result = await new ApiCaller(_mcsvcConfig.CoreUrl).PostAsync<Core.TeamModel, bool>("api/team",
                    new Core.TeamModel
                    {
                        Id = coreTeam.Id,
                        Name = newName,
                        PlayerIds = coreTeam.PlayerIds,
                        TournamentId = coreTeam.TournamentId
                    });

                Thread.Sleep(1000);
            }

            // update touranments names
            foreach (var coreTournament in coreTournaments)
            {
                string pattern = "\\d* - ";
                string newName = Regex.Replace(coreTournament.Name, pattern, string.Empty);

                var result = await new ApiCaller(_mcsvcConfig.CoreUrl).PostAsync<Core.TournamentModel, bool>("api/tournament",
                    new Core.TournamentModel
                    {
                        Id = coreTournament.Id,
                        Name = newName,
                        Description = coreTournament.Description,
                        ImageUrl = coreTournament.ImageUrl,
                        GameType = (GameType)coreTournament.GameType
                    });

                Thread.Sleep(1000);
            }

            return true;
        }
    }
}
