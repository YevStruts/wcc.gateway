using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.Identity;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController, Authorize]
    [Route("api/[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ILogger<SettingsController> _logger;
        private readonly IMediator _mediator;

        public SettingsController(ILogger<SettingsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public Task<SettingsModel> Get()
        {
            var userId = User.GetUserId();
            _logger.LogInformation($"User:{userId} requested personal settings", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetSettingsQuery(userId));
        }

        [HttpPost]
        public Task<bool> Post(SettingsModel model)
        {
            var userId = User.GetUserId();
            _logger.LogInformation($"User:{userId} saved nickname value:{model.Nickname}", DateTimeOffset.UtcNow);
            return _mediator.Send(new SaveSettingsQuery(userId, model));
        }
    }
}
