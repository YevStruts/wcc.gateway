using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.Game;
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
            _logger.LogInformation($"User:{User.GetUserId()} get's player Id:{id}", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetPlayerDetailQuery(id));
        }

        [HttpGet, Route("List")]
        public Task<IEnumerable<PlayerModel>> List()
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's list of players", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetPlayerListQuery());
        }

        [HttpGet, Route("Profile/{id}")]
        public Task<PlayerProfile> Profile(int id)
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's player profile", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetPlayerProfileQuery(id));
        }

        [HttpPost, Authorize, Route("Update")]
        public Task<bool> Update(PlayerModel model)
        {
            var userId = User.GetUserId();
            _logger.LogInformation($"User:{userId} saves game", DateTimeOffset.UtcNow);
            return _mediator.Send(new UpdatePlayerQuery(model, userId));
        }
    }
}
