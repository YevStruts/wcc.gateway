using MediatR;
using Microsoft.EntityFrameworkCore;
using OAuth2.Infrastructure;
using System;
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
            if (userinfo == null ||
                string.IsNullOrEmpty(userinfo.id) ||
                string.IsNullOrEmpty(userinfo.username) ||
                string.IsNullOrEmpty(userinfo.discriminator))
            {
                throw new Exception("Can't get user info.");
            }

            if (!userSignIn(userinfo.id,
                            userinfo.username,
                            userinfo.avatar ?? string.Empty,
                            userinfo.discriminator,
                            userinfo.email ?? string.Empty,
                            token))
                throw new Exception("Can't user sign in");

            var user = new UserModel
            {
                id = userinfo.id,
                username = userinfo.username,
                email = userinfo.email,
                avatar = userinfo.avatar,
                code = request.Code
            };

            return user;
        }

        private bool userSignIn(string externalId, string username, string avatar, string discriminator, string email, string token)
        {
            var user = _db.GetUserByExternalId(externalId);

            #region Pre-registerd users
            
            if (user == null)
            {
                user = _db.GetUserByUsername(username);
                if (user != null && user.ExternalId != "0")
                    return false;
            }

            #endregion
            
            if (user == null)
            {
                var role = _db.GetRole(3L);
                var newUser = new User
                {
                    ExternalId = externalId,
                    Username = username,
                    Avatar = avatar,
                    Discriminator = discriminator,
                    Email = email,
                    Token = token,
                    Role = role
                };
                if (!_db.AddUser(newUser))
                    return false;

                var newPlayer = new Player
                {
                    Name = username,
                    UserId = newUser.Id,
                    User = newUser
                };
                if (!_db.AddPlayer(newPlayer))
                    return false;
                return true;
            }
            
            user.Username = username;
            user.Avatar = avatar;
            user.Discriminator = discriminator;
            user.Email = email;
            user.Token = token;

            return _db.UpdateUser(user);
        }
    }
}
