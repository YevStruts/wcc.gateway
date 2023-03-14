using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        protected readonly ILogger<NewsController>? _logger;
        protected readonly IMediator? _mediator;

        public NewsController(ILogger<NewsController>? logger, IMediator? mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("{id}")]
        public Task<NewsModel> Get(long id)
        {
            var locale = Request.Headers["locale"].ToString() ?? "ua";
            _logger.LogInformation($"User:{User.GetUserId()} get's news Id:{id}", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetNewsDetailQuery(id, locale));
        }

        [HttpGet, Route("List")]
        public Task<IEnumerable<NewsModel>> List()
        {
            var locale = Request.Headers["locale"].ToString() ?? "ua";
            _logger.LogInformation($"User:{User.GetUserId()} get's news list", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetNewsListQuery(locale));
        }
    }
}
