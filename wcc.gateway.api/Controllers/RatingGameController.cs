using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using wcc.gateway.kernel.Models.RatingGame;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingGameController : ControllerBase
    {
        protected readonly ILogger<RatingGameController>? _logger;
        protected readonly IMediator? _mediator;

        public RatingGameController(ILogger<RatingGameController>? logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<RatingGame1x1Model>> Get()
        {
            return await _mediator.Send(new GetRatingGamesQuery());
        }

        [HttpGet, Route("{id}")]
        public Task<RatingGame1x1Model> Get(string id)
        {
            string ratingGameId = HttpUtility.UrlDecode(id);
            return _mediator.Send(new GetRatingGameQuery(ratingGameId));
        }

        [HttpPost]
        public async Task<bool> Post(RatingGame1x1Model ratingGame)
        {
            return await _mediator.Send(new SaveOrUpdateRatingGameQuery(ratingGame));
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            string ratingGameId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new DeleteRatingGameQuery(ratingGameId));
        }
    }
}
