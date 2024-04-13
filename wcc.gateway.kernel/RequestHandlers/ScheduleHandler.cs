using AutoMapper;
using MediatR;
using System.Collections.Specialized;
using System.Numerics;
using System.Web;
using wcc.gateway.data;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Communication.Core;
using wcc.gateway.kernel.Communication.Rating;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.Schedule;
using Microservices = wcc.gateway.kernel.Models.Microservices;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetScheduleQuery : IRequest<ScheduleModel>
    {
        public string TournamentId { get; }
        public long Page { get; }
        public long Count { get; }

        public GetScheduleQuery(string tournamentId, long page, long count)
        {
            TournamentId = tournamentId;
            Page = page;
            Count = count;
        }
    }

    public class ScheduleHandler :
        IRequestHandler<GetScheduleQuery, ScheduleModel>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly Microservices.Config _mcsvcConfig;

        public ScheduleHandler(IDataRepository db, Microservices.Config mcsvcConfig)
        {
            _db = db;
            _mcsvcConfig = mcsvcConfig;
        }

        public async Task<ScheduleModel> Handle(GetScheduleQuery request, CancellationToken cancellationToken)
        {
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters.Add("tournamentId", request.TournamentId);
            parameters.Add("page", request.Page.ToString());
            parameters.Add("count", request.Count.ToString());

            var games = await new ApiCaller(_mcsvcConfig.CoreUrl).GetAsync<List<GameData>>($"api/game?{parameters}");

            return new ScheduleModel();
        }
    }
}
