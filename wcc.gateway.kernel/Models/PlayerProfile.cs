namespace wcc.gateway.kernel.Models
{
    public class PlayerProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public int Age { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public DateTime Debut { get; set; }
        public DateTime LastFight { get; set; }
        public List<LastFightsList>? LastFightsList { get; set; }
    }

    public class LastFightsList
    {
        public DateTime Date { get; set; }
        public string? Name { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        // [ '#080', '#800', '#800', '#080', '#080', '#080']
        public List<string>? Last6 { get; set; }
        public string? Tournament { get; set; }
        // 1 - win, 0 - draw, -1 - loss
        public int Wld { get; set; }
    }
}
