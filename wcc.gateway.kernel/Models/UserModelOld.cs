using System.ComponentModel.DataAnnotations;
using wcc.gateway.Infrastructure;

namespace wcc.gateway.kernel.Models
{
    public class UserModelOld
    {
        public string ExternalId { get; set; }

        public string? Username { get; set; }

        public string? Avatar { get; set; }

        public string? Token { get; set; }

        public string? Discriminator { get; set; }

        public string? PlayerName { get; set; }
    }
}
