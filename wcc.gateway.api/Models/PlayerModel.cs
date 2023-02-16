namespace wcc.gateway.api.Models
{
    public class PlayerModel
    {
        public long Id { get; set; }

        public string? Name { get; set; }
    }

    public class PlayerGameListModel : PlayerModel
    {
        public int Score { get; set; }
    }

    public class PlayerRatingModel : PlayerModel
    {
        public string? Comment { get; set; }
        public int Score { get; set; }
    }
}
