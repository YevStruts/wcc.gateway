using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Models.C3;

namespace wcc.gateway.kernel.Helpers
{
    internal class CommonHelper
    {
        public static string GenerateToken()
        {
            const string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();

            string gameKey = string.Join("", Enumerable.Range(0, 16)
                .Select(_ => characters[random.Next(characters.Length)]))
                .Insert(4, "-")
                .Insert(9, "-")
                .Insert(14, "-");

            return gameKey;
        }

        public static string CreateGameResultPayload(List<Player> players, List<GameResultModel> gameResults, List<C3SaveRankModel> rankResults)
        {
            var payload = "{\"content\": null,\"embeds\":[{\"description\": \"Quick Game Result\",\"color\":2733953,\"fields\":[fields_template]}],\"username\":\"WCC Server\",\"avatar_url\": \"https://wcc-cossacks.s3.eu-central-1.amazonaws.com/images/wcc-logo-discord.png\",\"attachments\":[]}";
            var fields = string.Empty;
            foreach (var rankResult in rankResults)
            {
                var player = players.FirstOrDefault(p => p.Id == rankResult.PlayerId);
                var current = gameResults.FirstOrDefault(r => r.player_id == rankResult.PlayerId);
                if  (current != null && player != null)
                {
                    string res = current.result == (int)GameResult.WIN ? "W" :
                        current.result == (int)GameResult.LOSE ? "L" : "-";
                    fields += "{\"name\":\"" + player.Name + "\",\"value\":\"" + res + ": " + rankResult.OldScore + " -> " + rankResult.NewScore + "\"},";
                }
            }
            return payload.Replace("fields_template", fields.TrimEnd(','));
        }
    }
}
