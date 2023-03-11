using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly ILogger<RuleController> _logger;
        protected readonly IMediator? _mediator;

        public RuleController(ILogger<RuleController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public Task<RuleModel> Get(long id)
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's rule Id:{id}", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetRuleDetailQuery(id));
        }
    }
}
