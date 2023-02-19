using MediatR;
using Microsoft.AspNetCore.Mvc;
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
            return _mediator.Send(new GetNewsDetailQuery(id));
        }

        [HttpGet, Route("List")]
        public Task<IEnumerable<NewsModel>> List()
        {
            return _mediator.Send(new GetNewsListQuery());
        }
    }
}
