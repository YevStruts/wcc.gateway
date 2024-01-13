using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
