﻿namespace wcc.gateway.api.Models
{
    public class TournamentModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Image_url { get; internal set; }
        public int Count_players { get; set; }
        public DateTime Date_start { get; set; }
        public DateTime Date_created { get; set; }
    }
}
