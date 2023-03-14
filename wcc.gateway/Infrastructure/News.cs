using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using wcc.gateway.Localization;

namespace wcc.gateway.Infrastructure
{
    public class NewsBase : Entity
    {
        [Required]
        [StringLength(255, ErrorMessage = "The Name value cannot exceed 255 characters. ")]
        public string? Name { get; set; }

        [Required]
        [StringLength(5000, ErrorMessage = "The Description value cannot exceed 5000 characters. ")]
        public string? Description { get; set; }
    }

    [Table("News")]
    public class News : NewsBase
    {
        [Required]
        [StringLength(255, ErrorMessage = "The ImageUrl value cannot exceed 255 characters. ")]
        public string? ImageUrl { get; set; }

        public List<NewsTranslations> Translations { get; set; }
    }

    [Table("NewsTranslations")]
    public class NewsTranslations : NewsBase
    {
        [Required]
        [ForeignKey("News")]
        public long NewsId { get; set; }

        [Required]
        [ForeignKey("Language")]
        public long LanguageId { get; set; }

        public News News { get; set; }
        public Language Language { get; set; }
    }
}
