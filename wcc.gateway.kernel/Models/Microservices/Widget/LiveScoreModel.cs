namespace wcc.gateway.kernel.Models.Microservices.Widget
{
    public class LiveScoreModel
    {
        public string? Id { get; set; }
        public string? SideA { get; set; }
        public string? SideB { get; set; }
        public int ScoreA { get; set; }
        public int ScoreB { get; set; }

        #region settings
        public int Width { get; set; }
        public int MarginTop { get; set; }
        public int MarginRight { get; set; }
        #endregion settings
    }
}
