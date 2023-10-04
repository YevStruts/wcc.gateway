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
            var games = _db.GetLastFightsStatistics(request.Id, 1);

            var playersDto = _db.GetPlayers();
            var playerDto = playersDto.First(p => p.Id == request.Id);

            var model = _mapper.Map<PlayerProfile>(playerDto);
            model.Debut = games.OrderBy(g => g.Date).FirstOrDefault()?.Date ?? DateTime.MinValue;
            model.LastFight = games.OrderByDescending(g => g.Date).FirstOrDefault()?.Date ?? DateTime.MinValue;
            model.LastFightsList = new List<LastFightsList>();

            int wins = 0;
            int losses = 0;
            foreach (var game in games)
            {
                if (!game.GameName?.Contains("(TL)") ?? false)
                {
                    List<string> last6Fights = new List<string>();
                    if (game.LastFights != null)
                    {
                        foreach (var lastFight in game.LastFights.Split(','))
                        {
                            var fightCode = "#888";
                            if (lastFight.Trim() == "1")
                                fightCode = "#080";
                            else if (lastFight.Trim() == "-1")
                                fightCode = "#800";

                            last6Fights.Add(fightCode);
                        }
                    }

                    model.LastFightsList.Add(new LastFightsList
                    {
                        Date = game.Date,
                        Name = game.Name,
                        Wins = game.Wins ?? 0,
                        Losses = game.Losses ?? 0,
                        Last6 = last6Fights,
                        Tournament = game.Tournament,
                        Wld = game.Result ?? 0
                    });

                    if (game.Result == 1) wins++;
                    if (game.Result == -1) losses++;
                }
            }

            model.Wins = wins;
            model.Losses = losses;

            return model;
        }
    }
}
