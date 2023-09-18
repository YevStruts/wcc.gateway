using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Communication.Rating;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetPlayerDetailQuery : IRequest<PlayerModel>
    {
        public long Id { get; }

        public GetPlayerDetailQuery(long id)
        { 
            Id = id;
        }
    }

    public class GetPlayerListQuery : IRequest<IEnumerable<PlayerModel>>
    {
    }

    public class GetPlayerProfileQuery : IRequest<PlayerProfile>
    {
        public long Id { get; }

        public GetPlayerProfileQuery(long id)
        {
            Id = id;
        }
    }

    public class PlayerHandler :
        IRequestHandler<GetPlayerDetailQuery, PlayerModel>,
        IRequestHandler<GetPlayerListQuery, IEnumerable<PlayerModel>>,
        IRequestHandler<GetPlayerProfileQuery, PlayerProfile>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;

        public PlayerHandler(IDataRepository db)
        {
            _db = db;
        }

        public Task<PlayerModel> Handle(GetPlayerDetailQuery request, CancellationToken cancellationToken)
        {
            var player = _db.GetPlayer(request.Id);
            var model = _mapper.Map<PlayerModel>(player);
            return Task.FromResult(model);
        }

        public Task<IEnumerable<PlayerModel>> Handle(GetPlayerListQuery request, CancellationToken cancellationToken)
        {
            var players = _db.GetPlayers();
            var model = _mapper.Map<IEnumerable<PlayerModel>>(players);
            return Task.FromResult(model);
        }

        public async Task<PlayerProfile> Handle(GetPlayerProfileQuery request, CancellationToken cancellationToken)
        {
            var playersDto = _db.GetPlayers();
            var playerDto = playersDto.First(p => p.Id == request.Id);

            var gamesDto = _db.GetGames().Where(g => g.HUserId == playerDto.UserId || g.VUserId == playerDto.UserId).OrderBy(g => g.Scheduled).ToList();

            var model = _mapper.Map<PlayerProfile>(playerDto);
            model.LastFightsList = new List<LastFightsList>();
            foreach ( var gameDto in gamesDto )
            {
                int wld = getWinLossDraw(gameDto, playerDto.UserId);
                
                var opponent = gameDto.HUserId == playerDto.UserId ?
                    playersDto.First(p => p.UserId == gameDto.VUserId) :
                    playersDto.First(p => p.UserId == gameDto.HUserId);

                model.LastFightsList.Add(new LastFightsList
                {
                    Date = gameDto.Scheduled,
                    Name = opponent.Name,
                    Wins = 0,
                    Losses = 0,
                    Last6 = new string[] {},
                    Tournament = gameDto.Tournament.Name,
                    Wld = wld
                });
            }

            return model;
        }

        private int getWinLossDraw(Game game, long userId)
        {
            if (game.HUserId == userId)
            {
                return game.HScore > game.VScore ? 1 :
                       game.HScore < game.VScore ? -1 : 0;
            }
            // game.VUserId == userId)
            return game.VScore > game.HScore ? 1 :
                    game.VScore < game.HScore ? -1 : 0;
        }
    }
}
