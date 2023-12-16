using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Helpers;
using wcc.gateway.data;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        protected readonly ILogger<GameController>? _logger;
        protected readonly IMediator? _mediator;

        public GameController(ILogger<GameController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet, Route("{id}")]
        public Task<GameListModel> Get(int id)
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's game Id:{id}", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetGameDetailQuery(id));
        }

        [HttpGet, Route("Schedule/{tournamentId}")]
        public Task<IEnumerable<GameListModel>> Schedule(long tournamentId)
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's games list", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetGameListQuery(tournamentId));
        }

        [HttpPost, Authorize, Route("Save")]
        public Task SaveGame(GameListModel model)
        {
            var userId = User.GetUserId();
            _logger.LogInformation($"User:{userId} saves game", DateTimeOffset.UtcNow);

            return _mediator.Send(new UpdateGameQuery(model, userId));
        }

        [HttpPost, Authorize, Route("Add")]
        public Task AddGame(AddGameModel model)
        {
            var userId = User.GetUserId();
            _logger.LogInformation($"User:{userId} saves game", DateTimeOffset.UtcNow);

            return _mediator.Send(new AddGameQuery(model.TournamentId, model.GameType, userId));
        }
    }
}
