using wcc.gateway.Infrastructure;
using wcc.gateway.kernel.Models;

namespace wcc.gateway.kernel.Helpers
{
    internal static class FakeDataHelper
    {
        //internal static IEnumerable<GameList> GetGames()
        //{
        //    return new List<GameListModel>()
        //    {
        //        new GameListModel
        //        {
        //            Id = 1, OrderId = 2, Name = "Game 1", Scheduled = ((DateTimeOffset)(new DateTime(2023, 01, 01))).ToUnixTimeMilliseconds(),
        //            Home = new PlayerGameListModel { Id = 1, Name = "Player 1", Score = 0 },
        //            Visitor = new PlayerGameListModel { Id = 2, Name = "Player 2", Score = 2 },
        //            YoutubeUrls = new List<string>() { "#", "#" }
        //        },
        //        new GameListModel
        //        {
        //            Id = 2, OrderId = 3, Name = "Game 2", Scheduled = ((DateTimeOffset)(new DateTime(2023, 01, 01))).ToUnixTimeMilliseconds(),
        //            Home = new PlayerGameListModel { Id = 3, Name = "Player 3", Score = 2 },
        //            Visitor = new PlayerGameListModel { Id = 4, Name = "Player 4", Score = 1 },
        //            YoutubeUrls = new List<string>() { "#", "#", "#" }
        //        },
        //        new GameListModel
        //        {
        //            Id = 3, OrderId = 1, Name = "Game 3", Scheduled = ((DateTimeOffset)(new DateTime(2023, 01, 01))).ToUnixTimeMilliseconds(),
        //            Home = new PlayerGameListModel { Id = 2, Name = "Player 2", Score = 0 },
        //            Visitor = new PlayerGameListModel { Id = 3, Name = "Player 3", Score = 0 },
        //        }
        //    };
        //}

        internal static IEnumerable<NewsModel> GetNews()
        {
            return new List<NewsModel>
            {
                new NewsModel { Id = 1, Name = "What is Lorem Ipsum?", Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s,", Image_url = "https://i.picsum.photos/id/664/140/345.jpg?hmac=652GUsIUg395zIDLbpwU2hfstk5GQsLVMNFjyu7OIAc" },
                new NewsModel { Id = 2, Name = "Why do we use it?", Description = "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout.", Image_url = "https://i.picsum.photos/id/965/140/345.jpg?hmac=7-_CkBhBFDClyLYILPdeGyeBtmiRBVgBRexfaduhvo4" },
                new NewsModel { Id = 3, Name = "Where does it come from?", Description = "Contrary to popular belief, Lorem Ipsum is not simply random text. It has roots in a piece of classical Latin literature from 45 BC, making it over 2000 years old. ", Image_url = "https://i.picsum.photos/id/387/140/345.jpg?hmac=hZNcO1Q71wsCZBt0gsUxljbR4sKs8VuVHFt0YNZxOq0" },
                new NewsModel { Id = 4, Name = "Lorem ipsum dolor sit amet.", Description = "Ea quos minima eum laudantium eaque sit corrupti tempora. At suscipit doloribus ut iure voluptatibus qui odit tenetur sit voluptatem odio hic sint ipsam cum consequatur architecto. ", Image_url = "https://i.picsum.photos/id/87/387/140.jpg?hmac=GyXj2rw58Fc5tw6vFhPoOZbBOhgYNH5x1cHU8A2HLqU" },
                new NewsModel { Id = 5, Name = "Eos vero voluptas", Description = "At adipisci quos et voluptatem consequatur non dolor impedit. Ut similique iste et eaque quia ut ipsa neque aut quia quis ea maxime magnam.", Image_url = "https://i.picsum.photos/id/79/387/140.jpg?hmac=j4tWMWNUA8ZMAmY83Mj16VODn_aVR5IDGc81yMVGj_E" },
                new NewsModel { Id = 6, Name = "Rem officiis quod ut repellat", Description = " At totam odio sed temporibus voluptatem eos natus temporibus sit enim porro. Vel aperiam ipsum quo vitae nihil et fugiat iure! Aut dolorem quisquam est alias cupiditate ut culpa dolorem nam deleniti architecto ea itaque molestiae.", Image_url = "https://i.picsum.photos/id/591/387/140.jpg?hmac=v3SIsk5v-Nq_jeSyterdQtVRAIONY5VFhCO39GpCP2s" }
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

        //internal static IEnumerable<RuleModel> GetRules()
        //{
        //    return new List<RuleModel>
        //    {
        //        new RuleModel {
        //            Id = 1,
        //            Name = "General",
        //            Season = "Summer",
        //            MapShape = "Land",
        //            TerrainType = "Plain",
        //            StartingResources = "Thousands",
        //            Minerals = "Rich",
        //            MapSize = "Normal",
        //            StartOptions = "Default",
        //            BaloonOptions = "Default",
        //            Cannons = "Default",
        //            PeaceTime = "15 min",
        //            EighteenthCenturyOptions = "Default",
        //            Capture = "Default",
        //            DipCenterAndMarket = "Default",
        //            Allies = "Nearby",
        //            LimitOfPopulation = "Without limitation",
        //            GameSpeed = "Very Fast"
        //        }
        //    };
        //}

        internal static IEnumerable<Tournament> GetTournaments()
        {
            return new List<Tournament>
            {
                new Tournament { Id = 1, Name = "Lords of The Kingdom", ImageUrl = "https://assets.rockpapershotgun.com/images/16/sep/cos3.jpg", CountPlayers = 22, DateStart = new DateTime(2023, 1, 25), DateCreated = new DateTime(2022, 12, 19) }
            };
        }
    }
}
