using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;

        public UserController(ILogger<UserController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("WhoAmI")]
        public Task<WhoAmIModel> WhoAmI()
        {
            _logger.LogInformation($"User:{User.GetUserId()} requests WhoAmI", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetUserWhoAmIQuery(User.GetUserId()));
        }
    }
}
