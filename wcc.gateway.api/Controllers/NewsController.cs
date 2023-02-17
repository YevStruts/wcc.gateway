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
        private readonly ILogger<NewsController> _logger;
        private readonly IMediator _mediator;

        public NewsController(ILogger<NewsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("{id}")]
        public Task<NewsModel> Get(long id)
        {
            return _mediator.Send(new News { Id = id });
        }

        [HttpGet, Route("List")]
        public IEnumerable<NewsModel> List()
        {
            //return FakeDataHelper.GetNews();

            throw new NotImplementedException();
        }
    }
}
