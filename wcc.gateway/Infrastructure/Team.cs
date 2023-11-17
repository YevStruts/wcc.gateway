using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcc.gateway.Infrastructure
{
    [Table("Team")]
    public class Team : Entity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The Name value cannot exceed 100 characters. ")]
        public string? Name { get; set; }

        [ForeignKey("Tournament")]
        public long TournamentId { get; set; }

        public Tournament Tournament { get; set; }

        public List<Player> Players { get; set; }
    }
}
