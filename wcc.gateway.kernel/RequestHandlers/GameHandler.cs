﻿using AutoMapper;
using MediatR;
using wcc.gateway.data;
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

    public class GameHandler :
        IRequestHandler<GetGameDetailQuery, GameListModel>,
        IRequestHandler<GetGameListQuery, IEnumerable<GameListModel>>,
        IRequestHandler<UpdateGameQuery, bool>
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

            var gamesIds = gamesDto.Select(g => g.Id).ToList();
            var youtubes = _db.GetYoutubes().Where(g => gamesIds.Contains(g.GameId)).ToList();

            var fakePlayer = new Player()
            {
                Id = 0,
                Name = "TBD",
                UserId = 0
            };

            foreach (var game in games)
            {
                var gameDto = gamesDto.First(g => g.Id == game.Id);
                var hUserId = gameDto.HUserId;
                var hPLayerDto = playersDto.FirstOrDefault(p => p.UserId == hUserId);
                if (hPLayerDto == null)
                {
                    hPLayerDto = fakePlayer;
                }

                game.Home = _mapper.Map<PlayerGameListModel>(hPLayerDto);
                game.Home.Score = gameDto.HScore;

                var vUserId = gameDto.VUserId;
                var vPLayerDto = playersDto.FirstOrDefault(p => p.UserId == vUserId);
                if (vPLayerDto == null)
                {
                    vPLayerDto = fakePlayer;
                }

                game.Visitor = _mapper.Map<PlayerGameListModel>(vPLayerDto);
                game.Visitor.Score = gameDto.VScore;

                var ytUrl = youtubes.Where(y => y.GameId == game.Id);
                foreach (var yt in ytUrl)
                {
                    game.YoutubeUrls.Add(yt.Url);
                }
            }

            return Task.FromResult(games);
        }

        public async Task<bool> Handle(UpdateGameQuery request, CancellationToken cancellationToken)
        {
            var user = _db.GetUserByExternalId(request.ExternalUserId);
            if (user == null || user.RoleId == (long)Roles.User) return false;

            var gameDto = _db.GetGame(request.Game.Id);
            if (gameDto == null) return false;

            gameDto.Name = request.Game.Name;

            gameDto.Scheduled = DateTimeOffset.FromUnixTimeMilliseconds(request.Game.Scheduled).UtcDateTime;

            var hPlayer = _db.GetPlayer(request.Game.Home.Id);
            gameDto.HUserId = hPlayer.UserId;
            gameDto.HScore = request.Game.Home.Score;

            var vPlayer = _db.GetPlayer(request.Game.Visitor.Id);
            gameDto.VUserId = vPlayer.UserId;
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
                //try
                //{
                //    var gameModel = new Models.Microservices.Rating.GameModel
                //    {
                //        GameId = gameDto.Id,
                //        HPlayerId = hPlayer.Id,
                //        HScore = gameDto.HScore,
                //        VPlayerId = vPlayer.Id,
                //        VScore = gameDto.VScore
                //    };
                //    var response = await new ApiCaller(_ratingConfig.Url).PostAsync<Models.Microservices.Rating.GameModel, string>("api/Game/Save", gameModel);
                //}
                //catch (Exception ex)
                //{

                //}
                return true;
            }
            return false;
        }
    }
}
