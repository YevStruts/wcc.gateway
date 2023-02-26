using MediatR;
using Microsoft.EntityFrameworkCore;
using OAuth2.Infrastructure;
using System.Collections.Specialized;
using wcc.gateway.data;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.integrations.Discord;
using wcc.gateway.kernel.Models.Discord;

namespace wcc.gateway.kernel.RequestHandlers
{
    public class DiscordOAuthBase
    {
        public string ClientId { get; protected set; }
        public string ClientSecret { get; protected set; }
        public string RedirectUri { get; protected set; }
        public string Scope { get; protected set; }
    }

    public class GetAuthorizationUrlQuery : DiscordOAuthBase, IRequest<string>
    {
        public GetAuthorizationUrlQuery(string clientId, string clientSecret, string redirectUri, string scope)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            Scope = scope;
        }
    }

    public class GetDiscordUserQuery : DiscordOAuthBase, IRequest<UserModel>
    {
        public string Code { get; }

        public GetDiscordUserQuery(string clientId, string clientSecret, string redirectUri, string scope, string code)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            RedirectUri = redirectUri;
            Scope = scope;
            Code = code;
        }
    }

    public class DiscordHandler :
        IRequestHandler<GetAuthorizationUrlQuery, string>,
        IRequestHandler<GetDiscordUserQuery, UserModel>
    {
        private readonly ApplicationDbContext _context;

        public DiscordHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetAuthorizationUrlQuery request, CancellationToken cancellationToken)
        {
            // TODO: move to integrations ?
            var discordClient = new DiscordClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
            {
                ClientId = request.ClientId,
                ClientSecret = request.ClientSecret,
                RedirectUri = request.RedirectUri,
                Scope = request.Scope
            });

            var authorizationUrl = await discordClient.GetLoginLinkUriAsync("SomeStateValueYouWantToUse");
            
            return authorizationUrl;
        }

        public async Task<UserModel> Handle(GetDiscordUserQuery request, CancellationToken cancellationToken)
        {
            // TODO: move to integrations ?
            var discordClient = new DiscordClient(new RequestFactory(), new OAuth2.Configuration.ClientConfiguration
            {
                ClientId = request.ClientId,
                ClientSecret = request.ClientSecret,
                RedirectUri = request.RedirectUri,
                Scope = request.Scope
            });

            string token = await discordClient.GetTokenAsync(new NameValueCollection()
            {
                { "code", request.Code }
            });

            var userinfo = await discordClient.GetUserInfoAsync();

            // SignIn user
            var userDto = new User
            {
                ExternalId = userinfo.id,
                Username = userinfo.username,
                Avatar = userinfo.avatar,
                Code = request.Code,
                Token = token
            };
            _context.Users.Add(userDto);
            _context.SaveChanges();

            var playerDto = new Player
            {
                Name = userDto.Username,
                UserId = userDto.Id
            };
            _context.Players.Add(playerDto);
            _context.SaveChanges();

            var user = new UserModel
            {
                id = userinfo.id,
                username = userinfo.username,
                avatar = userinfo.avatar,
                code = request.Code
            };

            return user;
        }
    }
}
