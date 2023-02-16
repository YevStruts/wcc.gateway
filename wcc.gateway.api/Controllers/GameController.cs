using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Models;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;

        public GameController(ILogger<GameController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("{id}")]
        public GameModel Get(int id)
        {
            //return FakeDataHelper.GetGames().FirstOrDefault(n => n.Id == id) ??
            //    new GameModel() { Id = 0, OrderId = 0, Name = "Not Found" };

            throw new NotImplementedException();
        }

        [HttpGet, Route("Schedule/{tournamentId}")]
        public IEnumerable<GameListModel> Schedule(int tournamentId)
        {
            //return FakeDataHelper.GetGames();

            throw new NotImplementedException();
        }
    }
}
