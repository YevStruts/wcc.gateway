using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace wcc.gateway.Infrastructure
{
    [Table("news")]
    public class News
    {
        [Required]
        public long Id { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The Name value cannot exceed 255 characters. ")]
        public string? Name { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "The Description value cannot exceed 5000 characters. ")]
        public string? Description { get; set; }

        [Required]
        [StringLength(255, ErrorMessage = "The ImageUrl value cannot exceed 255 characters. ")]
        public string? ImageUrl { get; set; }
    }
}
