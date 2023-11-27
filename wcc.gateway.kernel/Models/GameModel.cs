using wcc.gateway.Infrastructure;

namespace wcc.gateway.kernel.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string? Name { get; set; }
        public long Scheduled { get; set; }
        public PlayerGameListModel? Home { get; set; }
        public PlayerGameListModel? Visitor { get; set; }
        public GameType GameType { get; set; }
    }

    public class GameListModel : GameModel
    {
        public List<string>? ReplayUrls { get; set; }
        public List<string>? YoutubeUrls { get; set; }
    }
}
