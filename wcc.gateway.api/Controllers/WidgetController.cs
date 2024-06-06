using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using wcc.gateway.kernel.Models.Results;
using wcc.gateway.kernel.Models.Widget;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WidgetController : ControllerBase
    {
        protected readonly ILogger<WidgetController>? _logger;
        protected readonly IMediator? _mediator;

        public WidgetController(ILogger<WidgetController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("livescore/{id}")]
        public async Task<LiveScoreModel> Get(string id)
        {
            string liveScoreId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new GetLiveScoreQuery(liveScoreId));
        }

        [HttpPost, Route("livescore")]
        public async Task<SaveOrUpdateResult<LiveScoreModel>> Post(LiveScoreModel liveScore)
        {
            return await _mediator.Send(new SaveOrUpdateLiveScoreQuery(liveScore));
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            string liveScoreId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new DeleteLiveScoreQuery(liveScoreId));
        }
    }
}
