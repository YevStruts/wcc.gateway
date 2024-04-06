using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.Infrastructure;

namespace wcc.gateway.kernel.Models.Core
{
    internal class TournamentModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public GameType GameType { get; set; }
    }
}
