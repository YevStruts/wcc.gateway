using AutoMapper;
using MediatR;
using System.Numerics;
using wcc.gateway.data;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using Microservices = wcc.gateway.kernel.Models.Microservices;
using Core = wcc.gateway.kernel.Models.Core;
using Rating = wcc.gateway.kernel.Models.Rating;
using System.Web;
using wcc.gateway.kernel.Models.Game;
using wcc.gateway.kernel.Models.Results;
using wcc.gateway.kernel.Communication.Core;
using wcc.gateway.kernel.Interfaces;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetGameDetailQuery : IRequest<GameListModelOld>
    {
        public long Id { get; }

        public GetGameDetailQuery(long id)
        {
            Id = id;
        }
    }

    public class GetGameListQuery : IRequest<IEnumerable<GameListModelOld>>
    {
        public long TournamentId { get; }

        public GetGameListQuery(long tournamentId)
        {
            TournamentId = tournamentId;
        }
    }

    public class UpdateGameQuery : IRequest<bool>
    {
        public GameListModelOld Game { get; }
        public string ExternalUserId { get; }

        public UpdateGameQuery(GameListModelOld game, string externalUserId)
        {
            Game = game;
            ExternalUserId = externalUserId;
        }
    }

    public class AddGameQuery : IRequest<bool>
    {
        public long TournamentId { get; }
        public GameType GameType { get; }
        public string ExternalUserId { get; }

        public AddGameQuery(long tournamentId, GameType gameType, string externalUserId)
        {
            TournamentId = tournamentId;
            GameType = gameType;
            ExternalUserId = externalUserId;
        }
    }

    public class EditGameQuery : IRequest<bool>
    {
        public Game Game { get; }
        public string ExternalUserId { get; }

        public EditGameQuery(Game game, string externalUserId)
        {
            Game = game;
            ExternalUserId = externalUserId;
        }
    }


    public class GetGameQuery : IRequest<GameModel>
    {
        public string GameId { get; private set; }

        public GetGameQuery(string gameId)
        {
            GameId = gameId;
        }
    }

    public class DeleteGameQuery : IRequest<bool>
    {
        public string Id { get; }
        public string ExternalUserId { get; }

        public DeleteGameQuery(string id, string externalUserId)
        {
            Id = id;
            ExternalUserId = externalUserId;
        }
    }

    public class SaveOrUpdateGameQuery : IRequest<bool>
    {
        public GameModel Game { get; }
        public SaveOrUpdateGameQuery(GameModel game)
        {
            this.Game = game;
        }
    }

    public class GameHandler :
        IRequestHandler<GetGameDetailQuery, GameListModelOld>,
        IRequestHandler<GetGameListQuery, IEnumerable<GameListModelOld>>,
        IRequestHandler<UpdateGameQuery, bool>,
        IRequestHandler<AddGameQuery, bool>,
        IRequestHandler<EditGameQuery, bool>,
        IRequestHandler<GetGameQuery, GameModel>,
        IRequestHandler<DeleteGameQuery, bool>,
        IRequestHandler<SaveOrUpdateGameQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly Microservices.Config _mcsvcConfig;
        private readonly ICache _cache;

        public GameHandler(IDataRepository db, Microservices.Config mcsvcConfig, ICache cache)
        {
            _db = db;
            _mcsvcConfig = mcsvcConfig;
            _cache = cache;
        }

        public async Task<GameListModelOld> Handle(GetGameDetailQuery request, CancellationToken cancellationToken)
        {
            var gameDto = _db.GetGame(request.Id);
            if (gameDto == null)
                throw new ArgumentNullException(nameof(gameDto));

            var players = _db.GetPlayers().ToList();
            var teams = _db.GetTeams().Where(t => t.TournamentId == gameDto.TournamentId).ToList();
            var youtubes = _db.GetYoutubes().Where(g => g.GameId == gameDto.Id).ToList();

            return mapGame(gameDto, players, teams, youtubes);
        }

        public async Task<IEnumerable<GameListModelOld>> Handle(GetGameListQuery request, CancellationToken cancellationToken)
        {
            var gamesDto = _db.GetGames().Where(g => g.TournamentId == request.TournamentId).ToList();

            var players = _db.GetPlayers().ToList();
            var teams = _db.GetTeams().Where(t => t.TournamentId == request.TournamentId).ToList();

            var gamesIds = gamesDto.Select(g => g.Id).ToList();
            var youtubes = _db.GetYoutubes().Where(g => gamesIds.Contains(g.GameId)).ToList();

            List<GameListModelOld> games = new List<GameListModelOld>();
            foreach (var gameDto in gamesDto)
            {
                games.Add(mapGame(gameDto, players, teams, youtubes));
            }

            return games;
        }

        private GameListModelOld mapGame(Game gameDto, List<Player> players, List<Team> teams, List<Youtube> youtubes)
        {
            var game = _mapper.Map<GameListModelOld>(gameDto);

            var hUserId = gameDto.HUserId;
            game.Home = game.GameType == GameType.Teams ?
                addTeam(teams, hUserId) :
                addPlayer(players, hUserId);

            game.Home.Score = gameDto.HScore;

            var vUserId = gameDto.VUserId;
            game.Visitor = game.GameType == GameType.Teams ?
                addTeam(teams, vUserId) :
                addPlayer(players, vUserId);
            game.Visitor.Score = gameDto.VScore;

            var ytUrl = youtubes.Where(y => y.GameId == game.Id);
            game.YoutubeUrls?.Clear();
            foreach (var yt in ytUrl)
            {
                game.YoutubeUrls?.Add(yt.Url ?? string.Empty);
            }

            return game;
        }

        private PlayerGameListModel addPlayer(IEnumerable<Player> players, long userId)
        {
            var player = players.FirstOrDefault(p => p.UserId == userId);
            if (player == null)
            {
                player = new Player()
                {
                    Id = 0,
                    Name = "TBD",
                    UserId = 0
                };
            }
            return _mapper.Map<PlayerGameListModel>(player);
        }

        private PlayerGameListModel addTeam(IEnumerable<Team> teams, long userId)
        {
            var team = teams.FirstOrDefault(p => p.Id == userId);
            if (team == null)
            {
                team = new Team()
                {
                    Id = 0,
                    Name = "TBD"
                };
            }
            return _mapper.Map<PlayerGameListModel>(team);
        }

        public async Task<bool> Handle(UpdateGameQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalUserId);
            if (user == null || user.RoleId == (long)Roles.User) return false;

            var gameDto = _db.GetGame(request.Game.Id);
            if (gameDto == null) return false;

            gameDto.Name = request.Game.Name;

            gameDto.Scheduled = DateTimeOffset.FromUnixTimeMilliseconds(request.Game.Scheduled).UtcDateTime;

            long hPlayerId = 0;

            long vPlayerId = 0;

            switch (gameDto.GameType)
            {
                case GameType.Individual:
                    {
                        var hPlayer = _db.GetPlayer(request.Game.Home.Id);
                        var vPlayer = _db.GetPlayer(request.Game.Visitor.Id);

                        hPlayerId = hPlayer.Id;
                        vPlayerId = vPlayer.Id;
                        gameDto.HUserId = hPlayer.UserId;
                        gameDto.VUserId = vPlayer.UserId;
                    }
                    break;
                case GameType.Teams:
                    {
                        hPlayerId = request.Game.Home.Id;
                        vPlayerId = request.Game.Visitor.Id;

                        gameDto.HUserId = hPlayerId;
                        gameDto.VUserId = vPlayerId;
                    }
                    break;
                default: 
                    return false;
            }

            gameDto.HScore = request.Game.Home.Score;
            gameDto.VScore = request.Game.Visitor.Score;

            if (gameDto.YoutubeUrls == null)
                gameDto.YoutubeUrls = new List<Youtube>() { };
            
            if (request.Game.YoutubeUrls == null || request.Game.YoutubeUrls.Count == 0)
            {
                gameDto.YoutubeUrls.Clear();
            }
            else
            {
                for (var i = 0; i < request.Game.YoutubeUrls.Count; i++)
                {
                    if (gameDto.YoutubeUrls.Count > i)
                    {
                        gameDto.YoutubeUrls[i].Url = request.Game.YoutubeUrls[i];
                    }
                    else
                    {
                        gameDto.YoutubeUrls.Add(new Youtube() { GameId = gameDto.Id, Url = request.Game.YoutubeUrls[i] });
                    }
                }
            }
            if (_db.UpdateGame(gameDto))
            {
                try
                {
                    var gameModel = new Models.Microservices.Rating.GameModel
                    {
                        GameId = gameDto.Id,
                        HPlayerId = hPlayerId,
                        HScore = gameDto.HScore,
                        VPlayerId = vPlayerId,
                        VScore = gameDto.VScore,
                        GameType = gameDto.GameType
                    };
                    if (gameDto.GameType == GameType.Teams)
                    {
                        Team hTeam = _db.GetTeam(request.Game.Home.Id);
                        Team vTeam = _db.GetTeam(request.Game.Visitor.Id);

                        gameModel.HParticipants = hTeam.Players.Select(p => p.Id).ToList();
                        gameModel.VParticipants = vTeam.Players.Select(p => p.Id).ToList();
                    }
                    var response = await new ApiCaller(_mcsvcConfig.RatingUrl).PostAsync<Models.Microservices.Rating.GameModel, string>("api/Game/Save", gameModel);
                }
                catch (Exception ex)
                {

                }
                return true;
            }
            return false;
        }

        public async Task<bool> Handle(AddGameQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalUserId);
            if (user == null || user.RoleId == (long)Roles.User) return false;

            var orderId = _db.GetGames().Where(g => g.TournamentId == request.TournamentId).OrderByDescending(g => g.OrderId).FirstOrDefault()?.OrderId + 1 ?? 1;

            var game = new Game
            {
                OrderId = orderId,
                Name = string.Empty,
                Scheduled = DateTime.UtcNow,
                TournamentId = request.TournamentId,
                GameType = request.GameType
            };

            return _db.AddGame(game);
        }

        public async Task<bool> Handle(EditGameQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalUserId);
            if (user == null || user.RoleId == (long)Roles.User) return false;
            //var game = _db.GetGame(request.Id);
            //game.Name = request.;
            throw new NotImplementedException();
        }

        public async Task<GameModel> Handle(GetGameQuery request, CancellationToken cancellationToken)
        {
            var game = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<Core.GameModel>($"api/game/{HttpUtility.UrlEncode(request.GameId)}");
            return _mapper.Map<GameModel>(game);
        }

        public async Task<bool> Handle(DeleteGameQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalUserId);
            if (user == null || user.RoleId == (long)Roles.User) return false;

            return
                await new ApiCaller(_mcsvcConfig.CoreUrl).DeleteAsync($"api/game/{HttpUtility.UrlEncode(request.Id)}") &&
                await new ApiCaller(_mcsvcConfig.RatingUrl).PostAsync<string, bool>($"api/game/ByCoreGameId/{HttpUtility.UrlEncode(request.Id)}", "");
        }

        public async Task<bool> Handle(SaveOrUpdateGameQuery request, CancellationToken cancellationToken)
        {
            var gameResult = await new ApiCaller(_mcsvcConfig.CoreUrl).PostAsync<Core.GameModel, SaveOrUpdateResult<Core.GameModel>>("api/game",
                new Core.GameModel
                {
                    Id = request.Game.Id,
                    GameType = request.Game.GameType,
                    SideA = request.Game.SideA,
                    SideB = request.Game.SideB,
                    ScoreA = request.Game.ScoreA,
                    ScoreB = request.Game.ScoreB,
                    TournamentId = request.Game.TournamentId,
                    Scheduled = request.Game.Scheduled,
                    Youtube = request.Game.YouTube,
                });

            if (gameResult.Success)
            {
                if (!request.Game.CountRating)
                    return true;

                await _cache.RemoveAsync("rating");
            }

            if (gameResult.Success)
            {
                return await new ApiCaller(_mcsvcConfig.RatingUrl).PostAsync<Rating.GameModel, bool>("api/game/save",
                    new Rating.GameModel
                    {
                        GameId = gameResult.Value.Id,
                        GameType = gameResult.Value.GameType,
                        SideA = gameResult.Value.SideA,
                        SideB = gameResult.Value.SideB,
                        ScoreA = gameResult.Value.ScoreA,
                        ScoreB = gameResult.Value.ScoreB
                    });
            }
            return false;
        }
    }
}
