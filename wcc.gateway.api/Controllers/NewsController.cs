using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Models;
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
        public Task<string> Get(int id)
        {
            //return FakeDataHelper.GetNews().FirstOrDefault(n => n.Id == id) ??
            //    new NewsItemModel() { Id = 0, Name = "Not Found", Description = "Empty", Image_url = "" };

            return _mediator.Send(new Ping { ResponseMessage = "Pong!" });
        }

        [HttpGet, Route("List")]
        public IEnumerable<NewsModel> List()
        {
            //return FakeDataHelper.GetNews();

            throw new NotImplementedException();
        }
    }
}
