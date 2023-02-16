using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace wcc.gateway.Infrastructure
{
    [Table("tournament")]
    public class Tournament
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Name value cannot exceed 100 characters. ")]
        public string? Name { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The ImageUrl value cannot exceed 255 characters. ")]
        public string? ImageUrl { get; set; }

        [Required]
        public int CountPlayers { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
