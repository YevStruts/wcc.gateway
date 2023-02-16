using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Models;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly ILogger<NewsController> _logger;

        public NewsController(ILogger<NewsController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("{id}")]
        public NewsModel Get(int id)
        {
            //return FakeDataHelper.GetNews().FirstOrDefault(n => n.Id == id) ??
            //    new NewsItemModel() { Id = 0, Name = "Not Found", Description = "Empty", Image_url = "" };

            throw new NotImplementedException();
        }

        [HttpGet, Route("List")]
        public IEnumerable<NewsModel> List()
        {
            //return FakeDataHelper.GetNews();

            throw new NotImplementedException();
        }
    }
}
