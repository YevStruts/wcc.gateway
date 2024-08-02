using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.kernel.Models.RatingGame.Enums;

namespace wcc.gateway.kernel.Models.Microservices.Rating.RatingGame
{
    public class RatingGame1x1Model
    {
        public string Id { get; set; }
        public ulong MessageId { get; set; }
        public ulong UserId { get; set; }
        public ulong ChannelId { get; set; }
        public GameStatus Status { get; set; }
        public DateTime Created { get; set; }
        public RatingGameSettings1x1Model Settings { get; set; }
    }
}
