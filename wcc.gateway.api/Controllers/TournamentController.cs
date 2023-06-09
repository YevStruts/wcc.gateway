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
            var locale = Request.Headers["locale"].ToString() ?? "gb";
            _logger.LogInformation($"User:{User.GetUserId()} get's tournament Id:{id}", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetTournamentDetailQuery(id, locale));
        }

        [HttpGet, Route("participant/{id}")]
        public Task<IEnumerable<PlayerModel>> GetParticipants(int id)
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's tournament's Id:{id} participants", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetTournamentParticipantsQuery(id));
        }

        [HttpGet, Route("getswitztable/{id}")]
        public Task<List<SwitzTableItemModel>> GetSwitzTable(int id)
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's tournament's Id:{id} Switz table", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetTournamentSwitzTableQuery(id));
        }

        [HttpPost, Route("Join/{id:int}")]
        public Task<bool> Join(int id)
        {
            var userId = User.GetUserId();
            _logger.LogInformation($"User:{userId} joins to tournament Id:{id}", DateTimeOffset.UtcNow);
            return _mediator.Send(new JoinToTournamentQuery(id, userId));
        }

        [HttpPost, Route("Leave/{id:int}")]
        public Task<bool> Leave(int id)
        {
            var userId = User.GetUserId();
            _logger.LogInformation($"User:{userId} leaves to tournament Id:{id}", DateTimeOffset.UtcNow);
            return _mediator.Send(new LeaveTournamentQuery(id, userId));
        }

        [AllowAnonymous]
        [HttpGet, Route("List")]
        public Task<IEnumerable<TournamentModel>> List()
        {
            var locale = Request.Headers["locale"].ToString() ?? "ua";
            _logger.LogInformation($"User:{User.GetUserId()} list of tournaments", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetTournamentListQuery(locale));
        }

        [HttpGet, Route("GetParticipationStatus/{tournamentId:int}")]
        public Task<bool> GetParticipationStatus(int tournamentId)
        {
            var userId = User.GetUserId();
            _logger.LogInformation($"User:{userId} get's participation status four tournament Id:{tournamentId}", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetParticipationStatusQuery(tournamentId, userId));
        }
    }
}
