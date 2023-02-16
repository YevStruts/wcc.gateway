namespace wcc.gateway.api.Models
{
    public class RuleModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        /* Settings */

        public string? Season { get; set; }
        public string? MapShape { get; set; }
        public string? TerrainType { get; set; }
        public string? StartingResources { get; set; }
        public string? Minerals { get; set; }
        public string? MapSize { get; set; }

        /* Additional? Options */

        public string? StartOptions { get; set; }
        public string? BaloonOptions { get; set; }
        public string? Cannons { get; set; }
        public string? PeaceTime { get; set; }
        public string? EighteenthCenturyOptions { get; set; }
        public string? Capture { get; set; }
        public string? DipCenterAndMarket { get; set; }
        public string? Allies { get; set; }
        public string? LimitOfPopulation { get; set; }
        public string? GameSpeed { get; set; }
    }
}
