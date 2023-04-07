using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using wcc.gateway.Infrastructure;

namespace wcc.gateway.Identity
{
    [Table("Users")]
    public class User : Entity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The ExternalId value cannot exceed 100 characters. ")]
        public string ExternalId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Username value cannot exceed 100 characters. ")]
        public string? Username { get; set; }

        [StringLength(100, ErrorMessage = "The Avatar value cannot exceed 100 characters. ")]
        public string? Avatar { get; set; }

        [Required]
        [StringLength(2048, ErrorMessage = "The Token value cannot exceed 2048 characters. ")]
        public string? Token { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The Discriminator value cannot exceed 32 characters. ")]
        public string? Discriminator { get; set; }

        [StringLength(100, ErrorMessage = "The Email value cannot exceed 100 characters. ")]
        public string? Email { get; set; }

        [Required]
        [ForeignKey("Role")]
        public long RoleId { get; set; }

        public Player Player { get; set; }

        public Role Role { get; set; }
    }
}
