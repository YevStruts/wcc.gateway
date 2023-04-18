using AutoMapper;
using MediatR;
using wcc.gateway.data;
using wcc.gateway.Infrastructure;
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

    public class PlayerHandler :
        IRequestHandler<GetPlayerDetailQuery, PlayerModel>,
        IRequestHandler<GetPlayerListQuery, IEnumerable<PlayerModel>>
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
    }
}
