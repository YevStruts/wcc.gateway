using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.Localization;

namespace wcc.gateway.Infrastructure
{
    public class RatingBase : Entity
    {
        [Required]
        [StringLength(255, ErrorMessage = "The Name value cannot exceed 255 characters. ")]
        public string? Name { get; set; }

        [StringLength(255, ErrorMessage = "The Comment value cannot exceed 255 characters. ")]
        public string? Comment { get; set; }
    }

    [Table("Rating")]
    public class Rating : RatingBase
    {
        public List<Player> Players { get; set; }
        public List<RatingTranslations> Translations { get; set; }
    }

    [Table("RatingTranslations")]
    public class RatingTranslations : RatingBase
    {
        [Required]
        [ForeignKey("Rating")]
        public long RatingId { get; set; }

        [Required]
        [ForeignKey("Language")]
        public long LanguageId { get; set; }

        public Rating Rating { get; set; }

        public Language Language { get; set; }
    }

    [Table("PlayerRatingData")]
    public class PlayerRatingData : Entity
    {
        public int Progress { get; set; }
        public int Position { get; set; }
        public int TotalPoints { get; set; }
        public Player Player { get; set; }
        public Rating Rating { get; set; }
    }
}
