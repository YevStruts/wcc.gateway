using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using Core = wcc.gateway.kernel.Models.Core;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class MigrateUserQuery : IRequest<bool>
    {
        public UserModel User { get; }

        public MigrateUserQuery(UserModel user)
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

    public class MigrationHandler : IRequestHandler<MigrateUserQuery, bool>,
        IRequestHandler<MigrateTeamsQuery, bool>,
        IRequestHandler<MigrateTournamentsQuery, bool>,
        IRequestHandler<MigrateGamesQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public MigrationHandler(IDataRepository db)
        {
            _db = db;
        }

        public async Task<bool> Handle(MigrateUserQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.User.ExternalId);
            if (user == null || user.Player == null) return false;

            var token = string.IsNullOrEmpty(user.Player.Token) ?
                CommonHelper.GenerateToken() : user.Player.Token;

            var playerData = await new ApiCaller("http://localhost:6003").PostAsync<Core.PlayerModel, bool>("api/player", 
                new Core.PlayerModel
                {
                    Name = request.User.PlayerName,
                    UserId = user.Id.ToString(),
                    IsActive = true,
                    Token = token
                });

            return true;
        }

        public async Task<bool> Handle(MigrateTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = _db.GetTeams();
            if (teams == null) return false;

            var playerData = await new ApiCaller("http://localhost:6003").GetAsync<List<Core.PlayerModel>>("api/player");

            Thread.Sleep(1000);

            foreach (var team in teams)
            {
                var userIds = team.Players.Select(u => u.UserId.ToString()).ToList();

                var players = playerData.Where(p => p.UserId != null && userIds.Contains(p.UserId)).Select(p => p.Id).ToList();

                if (players == null) return false;

                var result = await new ApiCaller("http://localhost:6003").PostAsync<Core.TeamModel, bool>("api/team",
                    new Core.TeamModel
                    {
                        Name = team.Name,
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

                var result = await new ApiCaller("http://localhost:6003").PostAsync<Core.TournamentModel, bool>("api/tournament",
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

            var corePlayers = await new ApiCaller("http://localhost:6003").GetAsync<List<Core.PlayerModel>>("api/player");
            Thread.Sleep(1000);

            var coreTeams = await new ApiCaller("http://localhost:6003").GetAsync<List<Core.PlayerModel>>("api/team");
            Thread.Sleep(1000);

            var coreTournaments = await new ApiCaller("http://localhost:6003").GetAsync<List<Core.PlayerModel>>("api/tournament");
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
                    sideA.AddRange(coreTeams.Where(p => p.UserId == game.HUserId.ToString()).Select(p => p.Id).ToList());
                    sideB.AddRange(coreTeams.Where(p => p.UserId == game.VUserId.ToString()).Select(p => p.Id).ToList());
                }
                else
                {
                    return false;
                }

                var tournamentId = coreTournaments.FirstOrDefault(t => t.Name.StartsWith(game.TournamentId.ToString() + " - "))?.Id;

                var result = await new ApiCaller("http://localhost:6003").PostAsync<Core.GameModel, bool>("api/game",
                    new Core.GameModel
                    {
                        GameType = type,
                        SideA = sideA,
                        SideB = sideB,
                        ScoreA = game.HScore,
                        ScoreB = game.VScore,
                        TournamentId = tournamentId
                    });

                if (!result)
                {
                    return false;
                }

                Thread.Sleep(1000);
            }

            return true;
        }
    }
}
