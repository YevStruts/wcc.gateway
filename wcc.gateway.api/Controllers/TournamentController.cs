using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ILogger<TournamentController> _logger;
        protected readonly IMediator? _mediator;

        public TournamentController(ILogger<TournamentController> logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("{id}")]
        public Task<TournamentModel> Get(int id)
        {
            return _mediator.Send(new GetTournamentDetailQuery(id));
        }

        [HttpGet, Route("List")]
        public Task<IEnumerable<TournamentModel>> List()
        {
            return _mediator.Send(new GetTournamentListQuery());
        }
    }
}
