using OAuth2.Client;
using OAuth2.Configuration;
using OAuth2.Infrastructure;
using OAuth2.Models;
using RestSharp;
using Newtonsoft.Json;
using Endpoint = OAuth2.Client.Endpoint;

namespace wcc.gateway.integrations.Discord
{
    public class DiscordClient : OAuth2Client
    {
        public override string Name => "Discord";

        public DiscordClient(IRequestFactory factory, IClientConfiguration configuration) : base(factory, configuration)
        {
        }

        protected override Endpoint AccessCodeServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = "https://discord.com/api",
                    Resource = "/oauth2/authorize"
                };
            }
        }

        protected override Endpoint AccessTokenServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = "https://discord.com/api",
                    Resource = "/oauth2/token"
                };
            }
        }

        protected override Endpoint UserInfoServiceEndpoint
        {
            get
            {
                return new Endpoint
                {
                    BaseUri = "https://discord.com/api",
                    Resource = "/users/@me"
                };
            }
        }

        protected override OAuth2.Models.UserInfo ParseUserInfo(string content)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UserInfo> GetUserInfoAsync()
        {
            var url = UserInfoServiceEndpoint.Uri;
            var client = new RestClient(url);
            client.AddDefaultHeader("Authorization", $"Bearer {AccessToken}");
            var request = new RestRequest(url, Method.GET);
            var response = await client.ExecuteAsync(request);
            return JsonConvert.DeserializeObject<UserInfo>(response.Content);
        }
    }
}
