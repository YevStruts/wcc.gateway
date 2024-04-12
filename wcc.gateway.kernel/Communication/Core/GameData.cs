using wcc.gateway.Infrastructure;

namespace wcc.gateway.kernel.Communication.Core
{
    internal class GameData
    {
        public string? Id { get; set; }
        public GameType GameType { get; set; }
        public List<string>? SideA { get; set; }
        public List<string>? SideB { get; set; }
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }
        public string? TournamentId { get; set; }
    }
}
