using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using wcc.gateway.Identity;

namespace wcc.gateway.Infrastructure
{
    [Table("Players")]
    public class Player : Entity
    {
        public Player()
        {
            CountryId = 254; /* Neutral */
            IsActive = true;
        }

        [Required]
        [StringLength(100, ErrorMessage = "The Name value cannot exceed 100 characters. ")]
        public string? Name { get; set; }

        [Required]
        [ForeignKey("User")]
        public long UserId { get; set; }

        public int Age { get; set; }

        [Required]
        public long CountryId { get; set; }

        public bool IsActive { get; set; }

        [StringLength(100, ErrorMessage = "The Name value cannot exceed 100 characters. ")]
        public string? Token { get; set; }

        public User User { get; set; }

        public List<Tournament> Tournament { get; set; }

        public List<Team> Teams { get; set; }
    }
}
