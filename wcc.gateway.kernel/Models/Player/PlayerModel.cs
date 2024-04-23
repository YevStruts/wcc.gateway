using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models.Player
{
    public class PlayerModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public bool IsActive { get; set; }
        public string? UserId { get; set; }
        public string? Token { get; set; }
        public int Games { get; set; }
        public int Wins { get; set; }
    }
}
