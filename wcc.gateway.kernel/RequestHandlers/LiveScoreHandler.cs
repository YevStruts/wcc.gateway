using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using wcc.gateway.data;
using wcc.gateway.kernel.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.Results;
using wcc.gateway.kernel.Models.Standing;
using wcc.gateway.kernel.Models.Widget;
using static System.Formats.Asn1.AsnWriter;
using Microservices = wcc.gateway.kernel.Models.Microservices;
using Widget = wcc.gateway.kernel.Models.Widget;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class GetLiveScoreQuery : IRequest<LiveScoreModel>
    {
        public string LiveScoreId { get; }

        public GetLiveScoreQuery(string liveScoreId)
        {
            LiveScoreId = liveScoreId;
        }
    }

    public class SaveOrUpdateLiveScoreQuery : IRequest<SaveOrUpdateResult<LiveScoreModel>>
    {
        public LiveScoreModel LiveScore { get; set; }

        public SaveOrUpdateLiveScoreQuery(LiveScoreModel liveScore)
        {
            this.LiveScore = liveScore;
        }
    }

    public class DeleteLiveScoreQuery : IRequest<bool>
    {
        public string LiveScoreId { get; set; }

        public DeleteLiveScoreQuery(string liveScoreId)
        {
            this.LiveScoreId = liveScoreId;
        }
    }

    public class LiveScoreHandler : IRequestHandler<GetLiveScoreQuery, LiveScoreModel>,
        IRequestHandler<SaveOrUpdateLiveScoreQuery, SaveOrUpdateResult<LiveScoreModel>>,
        IRequestHandler<DeleteLiveScoreQuery, bool>
    {
        private readonly IDataRepository _db;
        private readonly IMapper _mapper = MapperHelper.Instance;
        private readonly Microservices.Config _mcsvcConfig;

        public LiveScoreHandler(IDataRepository db, Microservices.Config mcsvcConfig)
        {
            _db = db;
            _mcsvcConfig = mcsvcConfig;
        }

        public async Task<LiveScoreModel> Handle(GetLiveScoreQuery request, CancellationToken cancellationToken)
        {
            var livescore = await new ApiCaller(_mcsvcConfig.WidgetUrl)
                .GetAsync<List<Widget.LiveScoreModel>>($"api/livescore/{HttpUtility.UrlEncode(request.LiveScoreId)}");
            return _mapper.Map<LiveScoreModel>(livescore);
        }

        public async Task<SaveOrUpdateResult<LiveScoreModel>> Handle(SaveOrUpdateLiveScoreQuery request, CancellationToken cancellationToken)
        {
            return await new ApiCaller(_mcsvcConfig.WidgetUrl).PostAsync<Widget.LiveScoreModel, SaveOrUpdateResult<Widget.LiveScoreModel>>("api/livescore",
                _mapper.Map<LiveScoreModel>(request.LiveScore));
        }

        public async Task<bool> Handle(DeleteLiveScoreQuery request, CancellationToken cancellationToken)
        {
            return await new ApiCaller(_mcsvcConfig.WidgetUrl).DeleteAsync($"api/livescore/{HttpUtility.UrlEncode(request.LiveScoreId)}");
        }
    }
}
