using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.Infrastructure
{
    public class LastFightsStatistics
    {
        [Key]
        public long GameId { get; set; }

        public string? GameName { get; set; }

        public long? PlayerId { get; set; }

        public DateTime Date { get; set; }

        public long? UserId { get; set; }

        public string? Name { get; set; }

        public string? Tournament { get; set; }

        public int? Wins { get; set; }

        public int? Losses { get; set; }

        public string? LastFights { get; set; }

        public int? Result { get; set; }

        public long? LanguageId { get; set; }
    }
}
