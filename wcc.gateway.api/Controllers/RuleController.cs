using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Models;

namespace wcc.gateway.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly ILogger<RuleController> _logger;

        public RuleController(ILogger<RuleController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{id}")]
        public RuleModel Get(int id)
        {
            //return FakeDataHelper.GetRules().FirstOrDefault(n => n.Id == id) ??
            //    new RuleModel() { Id = 0, Name = " Not Found" };

            throw new NotImplementedException();
        }
    }
}
