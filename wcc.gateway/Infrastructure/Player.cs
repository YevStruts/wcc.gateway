using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using wcc.gateway.Identity;

namespace wcc.gateway.Infrastructure
{
    [Table("players")]
    public class Player : Entity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The Name value cannot exceed 100 characters. ")]
        public string? Name { get; set; }

        [Required]
        [ForeignKey("User")]
        public long UserId { get; set; }

        public virtual User user { get; set; }
    }
}
