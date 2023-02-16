using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Models;

namespace wcc.gateway.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ILogger<TournamentController> _logger;

        public TournamentController(ILogger<TournamentController> logger)
        {
            _logger = logger;
        }

        [HttpGet, Route("{id}")]
        public TournamentModel Get(int id)
        {
            //return FakeDataHelper.GetTournaments().FirstOrDefault(n => n.Id == id) ??
            //    new TournamentModel() { Id = 0, Name = "Not Found", Image_url = "" };

            throw new NotImplementedException();
        }

        [HttpGet, Route("List")]
        public IEnumerable<TournamentModel> List()
        {
            //return FakeDataHelper.GetTournaments();

            throw new NotImplementedException();
        }
    }
}
