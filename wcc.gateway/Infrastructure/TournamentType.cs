using System.ComponentModel.DataAnnotations;

namespace wcc.gateway.Infrastructure
{
    public class TournamentType : Entity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The Name value cannot exceed 100 characters. ")]
        public string? Name { get; set; }

        public List<Tournament> Tournaments { get; set; }
    }
}
