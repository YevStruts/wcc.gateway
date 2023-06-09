namespace wcc.gateway.kernel.Models
{
    public class SwitzTableItemModel
    {
        //public int Id { get; set; }
        public string? Name { get; set; }
        public string? Avatar { get; set; }
        public int GamesCount { get; set; }
        public int ScoresWon { get; set; }
        public int ScoresLoss { get; set; }
        public float AverageRatingOppWon { get; set; }
        public float AverageRatingOppLoss { get; set; }
        public int Scores { get; set; }
    }
}
