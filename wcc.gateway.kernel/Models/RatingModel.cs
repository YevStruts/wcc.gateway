using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models
{
    public class RatingModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string? AvatarUrl { get; set; }
        public int Position { get; internal set; }
        public int Progress { get; internal set; }
        public int TotalPoints { get; internal set; }
    }
}
