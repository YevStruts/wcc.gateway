using wcc.gateway.Infrastructure;

namespace wcc.gateway.kernel.Models.Microservices.Rating
{
    public class GameModel
    {
        public long GameId { get; set; }
        public long HPlayerId { get; set; }
        public int HScore { get; set; }
        public long VPlayerId { get; set; }
        public int VScore { get; set; }
        public GameType GameType { get; set; }
        public List<long>? HParticipants { get; set; }
        public List<long>? VParticipants { get; set; }
    }
}
