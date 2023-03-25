using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

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

    public class GameHandler :
        IRequestHandler<GetGameDetailQuery, GameListModel>,
        IRequestHandler<GetGameListQuery, IEnumerable<GameListModel>>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public GameHandler(IDataRepository db)
        {
            _db = db;
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
            //var games = FakeDataHelper.GetGames();
            var gamesDto = _db.GetGames().ToList();
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
    }
}
