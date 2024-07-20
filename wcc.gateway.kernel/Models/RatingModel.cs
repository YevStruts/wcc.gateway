using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.kernel.Models
{
    public class RatingModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? AvatarUrl { get; set; }
        public int Position { get; set; }
        public int Progress { get; set; }
        public int TotalPoints { get; set; }
        public string? Nation { get; set; }
    }
}
