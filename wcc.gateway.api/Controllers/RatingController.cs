using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RatingController : ControllerBase
    {
        protected readonly ILogger<RatingController>? _logger;
        protected readonly IMediator? _mediator;

        public RatingController(ILogger<RatingController>? logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("{id}")]
        public Task<List<RatingModel>> Get(long id)
        {
            var locale = Request.Headers["locale"].ToString() ?? "ua";
            _logger.LogInformation($"User:{User.GetUserId()} get's news Id:{id}", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetRatingQuery(id, locale));
        }

        [HttpPost, Route("update")]
        public async Task<bool> Update()
        {
            return await _mediator.Send(new UpdateRatingQuery());
        }

        [HttpPost, Route("evolve")]
        public async Task<bool> Evolve()
        {
            return await _mediator.Send(new EvolveRatingQuery());
        }
    }
}
