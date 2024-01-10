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

        [AllowAnonymous]
        [HttpPost, Route("RequestToken")]
        public async Task<bool> Post(RequestTokenModel model)
        {
            if (model.BearerToken != "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c")
            {
                return false;
            }
            _logger.LogInformation($"Request token for user:{model.Username}", DateTimeOffset.UtcNow);
            return await _mediator.Send(new RequestTokenQuery(model.Username));
        }
    }
}
