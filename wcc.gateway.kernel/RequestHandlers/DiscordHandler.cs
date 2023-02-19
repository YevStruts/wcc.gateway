using MediatR;
using OAuth2.Infrastructure;
using System.Collections.Specialized;
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

            // TODO: token store ?
            string token = await discordClient.GetTokenAsync(new NameValueCollection()
            {
                { "code", request.Code }
            });

            var userinfo = await discordClient.GetUserInfoAsync();

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
