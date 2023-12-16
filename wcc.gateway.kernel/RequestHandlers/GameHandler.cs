using AutoMapper;
using MediatR;
using System.Numerics;
using wcc.gateway.data;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.Microservices;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetGameDetailQuery : IRequest<GameListModel>
    {
        public long Id { get; }

        public GetGameDetailQuery(long id)
        {
            Id = id;
        }
    }

    public class GetGameListQuery : IRequest<IEnumerable<GameListModel>>
    {
        public long TournamentId { get; }

        public GetGameListQuery(long tournamentId)
        {
            TournamentId = tournamentId;
        }
    }

    public class UpdateGameQuery : IRequest<bool>
    {
        public GameListModel Game { get; }
        public string ExternalUserId { get; }

        public UpdateGameQuery(GameListModel game, string externalUserId)
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

    public class GameHandler :
        IRequestHandler<GetGameDetailQuery, GameListModel>,
        IRequestHandler<GetGameListQuery, IEnumerable<GameListModel>>,
        IRequestHandler<UpdateGameQuery, bool>,
        IRequestHandler<AddGameQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly RatingConfig _ratingConfig;

        public GameHandler(IDataRepository db, RatingConfig ratingConfig)
        {
            _db = db;
            _ratingConfig = ratingConfig;
        }

        public Task<GameListModel> Handle(GetGameDetailQuery request, CancellationToken cancellationToken)
        {
            var gameDto = _db.GetGame(request.Id);
            if (gameDto == null)
                throw new ArgumentNullException(nameof(gameDto));

            var game = _mapper.Map<GameListModel>(gameDto);

            var playersDto = _db.GetPlayers();

            var hPLayerDto = playersDto.FirstOrDefault(p => p.UserId == gameDto.HUserId);
            if (hPLayerDto == null)
                throw new ArgumentNullException(nameof(hPLayerDto));

            game.Home = _mapper.Map<PlayerGameListModel>(hPLayerDto);
            
            var vPLayerDto = playersDto.FirstOrDefault(p => p.UserId == gameDto.VUserId);
            if (vPLayerDto == null)
                throw new ArgumentNullException(nameof(vPLayerDto));

            game.Visitor = _mapper.Map<PlayerGameListModel>(vPLayerDto);

            var youtubes = _db.GetYoutubes().Where(g => g.Id == game.Id).ToList();
            foreach(var yt in youtubes)
            {
                game.YoutubeUrls.Add(yt.Url);
            }

            return Task.FromResult(game);
        }
        public Task<IEnumerable<GameListModel>> Handle(GetGameListQuery request, CancellationToken cancellationToken)
        {
            var gamesDto = _db.GetGames().Where(g => g.TournamentId == request.TournamentId).ToList();
            var games = _mapper.Map<IEnumerable<GameListModel>>(gamesDto);

            var playersDto = _db.GetPlayers();
            var teamsDto = _db.GetTeams().Where(t => t.TournamentId == request.TournamentId);

            var gamesIds = gamesDto.Select(g => g.Id).ToList();
            var youtubes = _db.GetYoutubes().Where(g => gamesIds.Contains(g.GameId)).ToList();

            foreach (var game in games)
            {
                var gameDto = gamesDto.First(g => g.Id == game.Id);

                var hUserId = gameDto.HUserId;
                game.Home = game.GameType == GameType.Teams ?
                    addTeam(teamsDto, hUserId) :
                    addPlayer(playersDto, hUserId);

                game.Home.Score = gameDto.HScore;

                var vUserId = gameDto.VUserId;
                game.Visitor = game.GameType == GameType.Teams ?
                    addTeam(teamsDto, vUserId) :
                    addPlayer(playersDto, vUserId);
                game.Visitor.Score = gameDto.VScore;

                var ytUrl = youtubes.Where(y => y.GameId == game.Id);
                foreach (var yt in ytUrl)
                {
                    game.YoutubeUrls.Add(yt.Url);
                }
            }

            return Task.FromResult(games);
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
                    var response = await new ApiCaller(_ratingConfig.Url).PostAsync<Models.Microservices.Rating.GameModel, string>("api/Game/Save", gameModel);
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
    }
}
