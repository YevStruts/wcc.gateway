﻿using wcc.gateway.Infrastructure;

namespace wcc.gateway.kernel.Models
{
    public class TournamentModel
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public GameType GameType { get; set; }
    }

    public class TournamentModelOld
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image_url { get; internal set; }
        public int Count_players { get; set; }
        public List<PlayerModelOld> Participant { get; set; }
        public DateTime Date_start { get; set; }
        public DateTime Date_created { get; set; }
        public bool IsEnrollment { get; set; }
        public int TournamentTypeId { get; set; }
    }
}
