using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models.Core
{
    internal class TeamModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public List<string>? PlayerIds { get; set; }
        public long TournamentId { get; set; }
    }
}
