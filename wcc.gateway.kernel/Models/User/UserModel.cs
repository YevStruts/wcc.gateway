using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models.User
{
    public class UserModel
    {
        public long Id { get; set; }
        public string ExternalId { get; set; }

        public string? Username { get; set; }

        public string? Avatar { get; set; }

        public string? Email { get; set; }

        public long RoleId { get; set; }
    }
}
