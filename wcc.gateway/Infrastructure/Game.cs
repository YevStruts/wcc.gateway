using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.Identity;

namespace wcc.gateway.Infrastructure
{
    public class Game : Entity
    {
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public DateTime Scheduled { get; set; }
        
        public long HUserId { get; set; }
        public int HScore { get; set; }

        public long VUserId { get; set; }
        public int VScore { get; set; }

        public List<Youtube> YoutubeUrls { get; set; }
    }
}
