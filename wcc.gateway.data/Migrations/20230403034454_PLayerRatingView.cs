using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class PLayerRatingView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW PlayerRatingView as
SELECT dbo.Rating.Id, dbo.PlayerRating.PlayersId, dbo.PlayerRating.RatingId, dbo.PlayerRating.Position, dbo.PlayerRating.Progress, dbo.PlayerRating.TotalPoints
FROM dbo.Rating
INNER JOIN dbo.PlayerRating ON dbo.Rating.Id = dbo.PlayerRating.RatingId
INNER JOIN dbo.Languages ON dbo.Rating.Id = dbo.Languages.Id
INNER JOIN dbo.RatingTranslations ON dbo.Rating.Id = dbo.RatingTranslations.RatingId AND dbo.Languages.Id = dbo.RatingTranslations.LanguageId;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DROP VIEW PlayerRatingView;
");
        }
    }
}
