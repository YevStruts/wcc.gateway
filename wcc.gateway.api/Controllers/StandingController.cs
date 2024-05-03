using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.Infrastructure;
using Standing = wcc.gateway.kernel.Models.Standing;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StandingController : ControllerBase
    {
        protected readonly ILogger<StandingController>? _logger;
        protected readonly IMediator? _mediator;

        public StandingController(ILogger<StandingController>? logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("roundrobin/{id}")]
        public async Task<List<Standing.RRGameModel>> GetRoundRobin(int id)
        {
            return await _mediator.Send(new GetRoundRobinQuery(id));
        }
    }
}
