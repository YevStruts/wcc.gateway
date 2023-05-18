using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wcc.gateway.api.Helpers;
using wcc.gateway.api.Models.Discord;
using wcc.gateway.api.Models.Jwt;
using wcc.gateway.Identity;
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
        private readonly JwtConfig _jwtConfig;

        public DiscordController(ILogger<DiscordController> logger, DiscordConfig discordConfig, JwtConfig jwtConfig, IMediator mediator)
        {
            _logger = logger;
            _discordConfig = discordConfig;
            _jwtConfig = jwtConfig;
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
                _logger.LogInformation($"Discord Exchange: {model.code}", DateTimeOffset.UtcNow);

                var clientId = _discordConfig.ClientID?.Trim() ?? string.Empty;
                var clientSecret = _discordConfig.ClientSecret?.Trim() ?? string.Empty;
                var redirectUri = _discordConfig.RedirectUrl ?? string.Empty;
                var scope = "identify";
                var code = model.code ?? string.Empty;

                _logger.LogInformation($"Discord login attempt: {model.code}", DateTimeOffset.UtcNow);

                var user = await _mediator.Send(new GetDiscordUserQuery(clientId, clientSecret, redirectUri, scope, code));

                _logger.LogInformation($"Discord attempt to retreive token: {user.id} {user.username}", DateTimeOffset.UtcNow);

                string token = createToken(user.id, user.username, user.email);

                _logger.LogInformation($"Discord retrieved token: {user.id} token:{token}", DateTimeOffset.UtcNow);

                return Ok(new { token, user.id, user.username, user.avatar });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception message:{ex.Message}", DateTimeOffset.UtcNow);
            }
            return Ok();
        }

        private string createToken(string id, string username, string email)
        {
            var issuer = _jwtConfig.Issuer?.Trim();
            var audience = _jwtConfig.Audience?.Trim(); // builder.Configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Key?.Trim() ?? string.Empty);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", id),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Email, email ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString())
             }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
    }
}
