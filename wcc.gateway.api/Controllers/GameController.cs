using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        protected readonly ILogger<GameController>? _logger;
        protected readonly IMediator? _mediator;

        public GameController(ILogger<GameController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("{id}")]
        public Task<GameModel> Get(int id)
        {
            return _mediator.Send(new GetGameDetailQuery(id));
        }

        [HttpGet, Route("Schedule/{tournamentId}")]
        public Task<IEnumerable<GameListModel>> Schedule(long tournamentId)
        {
            return _mediator.Send(new GetGameListQuery(tournamentId));
        }
    }
}
