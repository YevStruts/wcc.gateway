using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.Helpers
{
    internal static class FakeDataHelper
    {
        internal static IEnumerable<GameListModelOld> GetGames()
        {
            return new List<GameListModelOld>()
            {
                new GameListModelOld
                {
                    Id = 1, OrderId = 2, Name = "Game 1", Scheduled = ((DateTimeOffset)(new DateTime(2023, 01, 01))).ToUnixTimeMilliseconds(),
                    Home = new PlayerGameListModel { Id = 1, Name = "Player 1", Score = 0 },
                    Visitor = new PlayerGameListModel { Id = 2, Name = "Player 2", Score = 2 },
                    YoutubeUrls = new List<string>() { "#", "#" }
                },
                new GameListModelOld
                {
                    Id = 2, OrderId = 3, Name = "Game 2", Scheduled = ((DateTimeOffset)(new DateTime(2023, 01, 01))).ToUnixTimeMilliseconds(),
                    Home = new PlayerGameListModel { Id = 3, Name = "Player 3", Score = 2 },
                    Visitor = new PlayerGameListModel { Id = 4, Name = "Player 4", Score = 1 },
                    YoutubeUrls = new List<string>() { "#", "#", "#" }
                },
                new GameListModelOld    
                {
                    Id = 3, OrderId = 1, Name = "Game 3", Scheduled = ((DateTimeOffset)(new DateTime(2023, 01, 01))).ToUnixTimeMilliseconds(),
                    Home = new PlayerGameListModel { Id = 2, Name = "Player 2", Score = 0 },
                    Visitor = new PlayerGameListModel { Id = 3, Name = "Player 3", Score = 0 },
                }
            };
        }

        internal static IEnumerable<PlayerRatingModel> GetPlayers()
        {
            return new List<PlayerRatingModel>
            {
                new PlayerRatingModel { Id = 0, Name = "vacant",           Comment = "" },
                new PlayerRatingModel { Id = 1, Name = "Terry Coleman",    Comment = "" },
                new PlayerRatingModel { Id = 2, Name = "William Hurst",    Comment = "" },
                new PlayerRatingModel { Id = 3, Name = "Stacy Paulsen",    Comment = "" },
                new PlayerRatingModel { Id = 4, Name = "Bernard Robinson", Comment = "" },
                new PlayerRatingModel { Id = 5, Name = "Nicholas Skelton", Comment = "" },
                new PlayerRatingModel { Id = 6, Name = "Louis Kimmel",     Comment = "" },
                new PlayerRatingModel { Id = 7, Name = "Kyle Turner",      Comment = "" },
                new PlayerRatingModel { Id = 8, Name = "John Coates",      Comment = "" },
                new PlayerRatingModel { Id = 9, Name = "Michael TrinIdad", Comment = "" },
                new PlayerRatingModel { Id =10, Name = "Dave Rubalcaba",   Comment = "" },
                new PlayerRatingModel { Id =11, Name = "Luis Harder",      Comment = "" },
                new PlayerRatingModel { Id =12, Name = "Shannon Brault",   Comment = "" },
                new PlayerRatingModel { Id =13, Name = "Charles Shelton",  Comment = "" },
                new PlayerRatingModel { Id =14, Name = "Richard Wiggins",  Comment = "" },
                new PlayerRatingModel { Id =15, Name = "Alan Hamilton",    Comment = "" },
                new PlayerRatingModel { Id =16, Name = "Grace Gutierrez",  Comment = "" },
            };
        }

        internal static IEnumerable<RuleModel> GetRules()
        {
            return new List<RuleModel>
            {
                new RuleModel {
                    Id = 1,
                    Name = "General",
                    Season = "Summer",
                    MapShape = "Land",
                    TerrainType = "Plain",
                    StartingResources = "Thousands",
                    Minerals = "Rich",
                    MapSize = "Normal",
                    StartOptions = "Default",
                    BaloonOptions = "Default",
                    Cannons = "Default",
                    PeaceTime = "15 min",
                    EighteenthCenturyOptions = "Default",
                    Capture = "Default",
                    DipCenterAndMarket = "Default",
                    Allies = "Nearby",
                    LimitOfPopulation = "Without limitation",
                    GameSpeed = "Very Fast"
                }
            };
        }
    }
}
