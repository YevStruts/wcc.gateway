using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MigrationController : ControllerBase
    {
        private readonly ILogger<MigrationController> _logger;
        private readonly IMediator _mediator;

        public MigrationController(ILogger<MigrationController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost, Route("players")]
        public async Task<bool> MigrateUsers(UserModel players)
        {
            return await _mediator.Send(new MigratePlayerQuery(players));
        }

        [HttpPost, Route("teams")]
        public async Task<bool> MigrateTeams(bool state)
        {
            return await _mediator.Send(new MigrateTeamsQuery());
        }

        [HttpPost, Route("tournaments")]
        public async Task<bool> MigrateTournaments()
        {
            return await _mediator.Send(new MigrateTournamentsQuery());
        }

        [HttpPost, Route("games")]
        public async Task<bool> MigrateGames()
        {
            return await _mediator.Send(new MigrateGamesQuery());
        }
    }
}
