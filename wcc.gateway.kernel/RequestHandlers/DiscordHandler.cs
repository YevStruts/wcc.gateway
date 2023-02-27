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
        private readonly IDataRepository _db;

        public DiscordHandler(IDataRepository db)
        {
            _db = db;
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

            if (!userSignIn(userinfo.id, userinfo.username, userinfo.avatar, request.Code, token))
                throw new Exception("Can't user sign in");

            var user = new UserModel
            {
                id = userinfo.id,
                username = userinfo.username,
                avatar = userinfo.avatar,
                code = request.Code
            };

            return user;
        }

        private bool userSignIn(string externalId, string username, string avatar, string code, string token)
        {
            var user =_db.GetUserByExternalId(externalId);
            if (user == null)
            {
                var newUser = new User
                {
                    ExternalId = externalId,
                    Username = username,
                    Avatar = avatar,
                    Code = code,
                    Token = token
                };
                if (!_db.AddUser(newUser))
                    return false;

                var newPlayer = new Player
                {
                    Name = username,
                    UserId = newUser.Id,
                    user = newUser
                };
                if (!_db.AddPlayer(newPlayer))
                    return false;
                return true;
            }
            return _db.UpdateUser(user);
        }
    }
}
