using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace wcc.gateway.Infrastructure
{
    [Table("players")]
    public class Player
    {
        [Required]
        public long Id { get; set; }

        [Key]
        [Required]
        public long UserId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The Name value cannot exceed 100 characters. ")]
        public string? Name { get; set; }
    }
}
