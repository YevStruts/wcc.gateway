using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;
        private readonly IMediator _mediator;

        public PlayerController(ILogger<PlayerController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("{id}")]
        public Task<PlayerModel> Get(int id)
        {
            return _mediator.Send(new GetPlayerDetailQuery(id));
        }

        [HttpGet, Route("List")]
        public Task<IEnumerable<PlayerModel>> List()
        {
            return _mediator.Send(new GetPlayerListQuery());
        }
    }
}
