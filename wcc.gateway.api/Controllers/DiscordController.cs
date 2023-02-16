using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using wcc.gateway.api.Models.Discord;

namespace wcc.gateway.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors("CorsApi", "*", "GET,POST")]
    public class DiscordController : ControllerBase
    {
        private readonly ILogger<DiscordController> _logger;
        private readonly DiscordConfig _discordConfig;

        public DiscordController(ILogger<DiscordController> logger, DiscordConfig discordConfig)
        {
            _logger = logger;
            _discordConfig = discordConfig;
        }

        [HttpGet, Route("Authorize")]
        public async Task<IActionResult> Authorize()
        {
            //var discordClient = new DiscordClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
            //{
            //    ClientId = _discordConfig.ClientID?.Trim(),
            //    ClientSecret = _discordConfig.ClientSecret?.Trim(),
            //    RedirectUri = _discordConfig.RedirectUrl,
            //    Scope = "identify"
            //});

            //return Ok(new
            //{
            //    RedirectUrl = await discordClient.GetLoginLinkUriAsync("SomeStateValueYouWantToUse")
            //});

            throw new NotImplementedException();
        }

        [HttpPost, Route("Exchange")]
        public async Task<IActionResult> Exchange(ExchangeModel model)
        {
            //var discordClient = new DiscordClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
            //{
            //    ClientId = _discordConfig.ClientID?.Trim(),
            //    ClientSecret = _discordConfig.ClientSecret?.Trim(),
            //    RedirectUri = _discordConfig.RedirectUrl,
            //    Scope = "identify"
            //});

            //string token = await discordClient.GetTokenAsync(new NameValueCollection()
            //{
            //    { "code", model.code }
            //});

            //var user = await discordClient.GetUserInfoAsync();

            //return Ok(new { model.code, user.id, user.username, user.avatar });
            
            throw new NotImplementedException();
        }
    }
}
