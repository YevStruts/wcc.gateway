namespace wcc.gateway.kernel.Models.Widget
{
    public class LiveScoreModel
    {
        public string? id { get; set; }
        public string? sideA { get; set; }
        public string? sideB { get; set; }
        public int scoreA { get; set; }
        public int scoreB { get; set; }

        #region settings
        public int width { get; set; }
        public int marginTop { get; set; }
        public int marginRight { get; set; }
        #endregion settings
    }
}
