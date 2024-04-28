using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.Infrastructure;

namespace wcc.gateway.kernel.Models.Rating
{
    public class GameModel
    {
        public string? GameId { get; set; }
        public GameType GameType { get; set; }
        public List<string>? SideA { get; set; }
        public List<string>? SideB { get; set; }
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }
    }
}
