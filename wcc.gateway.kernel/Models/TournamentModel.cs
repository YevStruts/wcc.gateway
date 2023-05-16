namespace wcc.gateway.kernel.Models
{
    public class TournamentModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image_url { get; internal set; }
        public int Count_players { get; set; }
        public List<PlayerModel> Participant { get; set; }
        public DateTime Date_start { get; set; }
        public DateTime Date_created { get; set; }
        public bool IsEnrollment { get; set; }
    }
}
