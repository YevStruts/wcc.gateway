using wcc.gateway.kernel.Models.RatingGame.Enums;

namespace wcc.gateway.kernel.Models.RatingGame
{
    public class RatingGameSettings1x1Model
    {
        public string Nation { get; set; }
        public WCCOptions Option { get; set; }
        public OpponentRating OpponentRating { get; set; }
        public Availability Availability { get; set; }
        public WinRule GameType { get; set; }
    }
}
