using wcc.gateway.kernel.Models;

namespace wcc.gateway.api.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public long Scheduled { get; set; }
        public PlayerGameListModel Home { get; set; }
        public PlayerGameListModel Visitor { get; set; }
    }

    public class GameListModel : GameModel
    {
        public List<string> ReplayUrls { get; set; }
        public List<string> YoutubeUrls { get; set; }
    }
}
