using wcc.gateway.Infrastructure;

namespace wcc.gateway.kernel.Models.Game
{
    public class AddGameModel
    {
        public long TournamentId { get; set; }
        public GameType GameType { get; set; }
    }
}
