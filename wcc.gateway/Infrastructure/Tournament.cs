using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using wcc.gateway.Localization;

namespace wcc.gateway.Infrastructure
{
    public class TouranmentBase : Entity
    {
        [Required]
        [StringLength(100, ErrorMessage = "The Name value cannot exceed 100 characters. ")]
        public string? Name { get; set; }

        [Required]
        [StringLength(10000, ErrorMessage = "The Name value cannot exceed 10000 characters. ")]
        public string? Description { get; set; }
    }

    [Table("Tournament")]
    public class Tournament : TouranmentBase
    {
        public Tournament()
        {
            Participant = new List<Player>();
        }

        [Required]
        [StringLength(255, ErrorMessage = "The ImageUrl value cannot exceed 255 characters. ")]
        public string? ImageUrl { get; set; }

        [Required]
        public int CountPlayers { get; set; }

        [Required]
        public DateTime DateStart { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public List<Player> Participant { get; set; }

        public List<TournamentTranslations> Translations { get; set; }
    }

    [Table("TournamentTranslations")]
    public class TournamentTranslations : TouranmentBase
    {
        [Required]
        [ForeignKey("Tournament")]
        public long TournamentId { get; set; }

        [Required]
        [ForeignKey("Language")]
        public long LanguageId { get; set; }

        public Tournament Tournament { get; set; }
        public Language Language { get; set; }
    }
}
