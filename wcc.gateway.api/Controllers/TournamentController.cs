using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TournamentController : ControllerBase
    {
        private readonly ILogger<TournamentController> _logger;
        protected readonly IMediator? _mediator;

        public TournamentController(ILogger<TournamentController> logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpGet, Route("{id}")]
        public Task<TournamentModel> Get(int id)
        {
            return _mediator.Send(new GetTournamentDetailQuery(id));
        }

        [HttpPost, Route("Join/{id:int}")]
        public Task<bool> Join(int id)
        {
            var userId = User.GetUserId();
            return _mediator.Send(new JoinToTournamentQuery(id, userId));
        }

        [HttpPost, Route("Leave/{id:int}")]
        public Task<bool> Leave(int id)
        {
            var userId = User.GetUserId();
            return _mediator.Send(new LeaveTournamentQuery(id, userId));
        }

        [AllowAnonymous]
        [HttpGet, Route("List")]
        public Task<IEnumerable<TournamentModel>> List()
        {
            return _mediator.Send(new GetTournamentListQuery());
        }

        [HttpGet, Route("GetParticipationStatus/{tournamentId:int}")]
        public Task<bool> GetParticipationStatus(int tournamentId)
        {
            var userId = User.GetUserId();
            return _mediator.Send(new GetParticipationStatusQuery(tournamentId, userId));
        }
    }
}
