using wcc.gateway.kernel.Models.RatingGame.Enums;
using WinRule = wcc.gateway.kernel.Models.RatingGame.Enums.WinRule;

namespace wcc.gateway.kernel.Models.Microservices.Rating.RatingGame
{
    public class RatingGameSettings1x1Model
    {
        public string Nation { get; set; }
        public WCCOptions Option { get; set; }
        public OpponentRating OpponentRating { get; set; }
        public Availability Availability { get; set; }
        public WinRule WinRule { get; set; }
    }
}
