using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models
{
    public class RegisterPlayerModel
    {
        public ulong ExternalId { get; set; }
        public string Username { get; set; }
        public string Avatar { get; set; }
        public string Token { get; set; }
        public string Discriminator { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string BearerToken { get; set; }
        public string GlobalName { get; set; }
    }
}
