using MediatR;
using Microsoft.AspNetCore.Mvc;
using wcc.gateway.api.Models.Discord;
using wcc.gateway.kernel.RequestHandlers;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors("CorsApi", "*", "GET,POST")]
    public class DiscordController : ControllerBase
    {
        private readonly ILogger<DiscordController> _logger;
        protected readonly IMediator? _mediator;
        private readonly DiscordConfig _discordConfig;

        public DiscordController(ILogger<DiscordController> logger, DiscordConfig discordConfig, IMediator mediator)
        {
            _logger = logger;
            _discordConfig = discordConfig;
            _mediator = mediator;
        }

        [HttpGet, Route("Authorize")]
        public async Task<IActionResult> Authorize()
        {
            var clientId = _discordConfig.ClientID?.Trim();
            var clientSecret = _discordConfig.ClientSecret?.Trim();
            var redirectUri = _discordConfig.RedirectUrl;
            var scope = "identify";

            var redirectUrl = await _mediator.Send(new GetAuthorizationUrlQuery(clientId, clientSecret, redirectUri, scope));

            return Ok(new { RedirectUrl = redirectUrl });
        }

        [HttpPost, Route("Exchange")]
        public async Task<IActionResult> Exchange(ExchangeModel model)
        {
            try
            {
                var clientId = _discordConfig.ClientID?.Trim() ?? string.Empty;
                var clientSecret = _discordConfig.ClientSecret?.Trim() ?? string.Empty;
                var redirectUri = _discordConfig.RedirectUrl ?? string.Empty;
                var scope = "identify";
                var code = model.code ?? string.Empty;

                var user = await _mediator.Send(new GetDiscordUserQuery(clientId, clientSecret, redirectUri, scope, code));

                return Ok(new { model.code, user.id, user.username, user.avatar });
            }
            catch (Exception ex)
            {

            }
            return Ok();
        }
    }
}
