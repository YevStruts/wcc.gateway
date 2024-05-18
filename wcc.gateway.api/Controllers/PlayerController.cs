using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.Identity;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.Game;
using wcc.gateway.kernel.Models.Player;
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

        [HttpGet, Route("ByUserId/{userId}")]
        public Task<PlayerModel> GetByUserId(string userId)
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's player by userId:{userId}", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetPlayerByUserIdQuery(userId));
        }

        [HttpGet]
        public async Task<IEnumerable<PlayerModel>> Get()
        {
            return await _mediator.Send(new GetPlayersQuery());
        }

        [HttpGet, Route("{id}")]
        public Task<PlayerModelOld> Get(int id)
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's player Id:{id}", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetPlayerDetailQuery(id));
        }

        [HttpGet, Route("List")]
        public Task<IEnumerable<PlayerModelOld>> List()
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
        public Task<bool> Update(PlayerModelOld model)
        {
            var userId = User.GetUserId();
            _logger.LogInformation($"User:{userId} saves game", DateTimeOffset.UtcNow);
            return _mediator.Send(new UpdatePlayerQuery(model, userId));
        }

        [AllowAnonymous]
        [HttpPost, Route("Register")]
        public async Task<bool> Post(RegisterPlayerModel model)
        {
            if (model.BearerToken != "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c")
            {
                return false;
            }
            return await _mediator.Send(new RegisterPlayerQuery(model));
        }
    }
}
