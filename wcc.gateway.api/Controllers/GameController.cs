using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using wcc.gateway.api.Helpers;
using wcc.gateway.data;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Models;
using wcc.gateway.kernel.Models.Game;
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

        [HttpPost]
        public async Task<bool> Post(GameModel game)
        {
            return await _mediator.Send(new SaveOrUpdateGameQuery(game));
        }

        [HttpDelete("{id}")]
        public async Task<bool> Delete(string id)
        {
            var userId = User.GetUserId();
            _logger.LogInformation($"User:{userId} saves game", DateTimeOffset.UtcNow);

            string gameId = HttpUtility.UrlDecode(id);
            return await _mediator.Send(new DeleteGameQuery(gameId, userId));
        }

        #region old
        [HttpGet, Route("{id}")]
        public Task<GameListModelOld> Get(int id)
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's game Id:{id}", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetGameDetailQuery(id));
        }

        [HttpGet, Route("Schedule/{tournamentId}")]
        public Task<IEnumerable<GameListModelOld>> Schedule(long tournamentId)
        {
            _logger.LogInformation($"User:{User.GetUserId()} get's games list", DateTimeOffset.UtcNow);
            return _mediator.Send(new GetGameListQuery(tournamentId));
        }

        [HttpPost, Authorize, Route("Save")]
        public Task SaveGame(GameListModelOld model)
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

        [HttpPost, Authorize, Route("Edit")]
        public Task EditGame(EditGameModel model)
        {
            var userId = User.GetUserId();
            _logger.LogInformation($"User:{userId} saves game", DateTimeOffset.UtcNow);

            return _mediator.Send(new EditGameQuery(new Game(), userId));
        }

        //[HttpPost, Authorize, Route("Delete")]
        //public Task EditGame(DeleteGameModel model)
        //{
        //    var userId = User.GetUserId();
        //    _logger.LogInformation($"User:{userId} saves game", DateTimeOffset.UtcNow);

        //    return _mediator.Send(new DeleteGameQuery(model.Id, userId));
        //}
        #endregion old
    }
}
