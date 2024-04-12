using AutoMapper;
using MediatR;
using System.Numerics;
using wcc.gateway.data;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Communication.Core;
using wcc.gateway.kernel.Communication.Rating;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using Microservices = wcc.gateway.kernel.Models.Microservices;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetScheduleQuery : IRequest
    {
        public long Page { get; }

        public GetScheduleQuery(long page)
        {
            Page = page;
        }
    }

    public class ScheduleHandler :
        IRequestHandler<GetScheduleQuery>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly Microservices.Config _mcsvcConfig;

        public ScheduleHandler(IDataRepository db, Microservices.Config mcsvcConfig)
        {
            _db = db;
            _mcsvcConfig = mcsvcConfig;
        }

        public async Task Handle(GetScheduleQuery request, CancellationToken cancellationToken)
        {
            var games = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<GameData>>("api/game");
        }
    }
}
