using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Models;
using wcc.gateway.kernel.Helpers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly ILogger<PlayerController> _logger;

        public PlayerController(ILogger<PlayerController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("{id}")]
        public PlayerModel Get(int id)
        {
            //var playersList = FakeDataHelper.GetPlayers();
            //return playersList.FirstOrDefault(n => n.Id == id) ??
            //    new PlayerModel() { Id = 0, Name = "Not Found" };

            throw new NotImplementedException();
        }

        [HttpGet, Route("List")]
        public IEnumerable<PlayerModel> List()
        {
            //return FakeDataHelper.GetPlayers();

            throw new NotImplementedException();
        }
    }
}
