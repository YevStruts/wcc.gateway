using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedPlayerRatingView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerRating");

            migrationBuilder.Sql(@"drop view PlayerRatingView;");

            migrationBuilder.DropTable(
                name: "RatingTranslations");

            migrationBuilder.DropTable(
                name: "Rating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW PlayerRatingView as
SELECT dbo.PlayerRating.PlayersId, dbo.PlayerRating.RatingId, dbo.PlayerRating.Position, dbo.PlayerRating.Progress, dbo.PlayerRating.TotalPoints
FROM dbo.Rating
INNER JOIN dbo.PlayerRating ON dbo.Rating.Id = dbo.PlayerRating.RatingId
INNER JOIN dbo.Languages ON dbo.Rating.Id = dbo.Languages.Id
INNER JOIN dbo.RatingTranslations ON dbo.Rating.Id = dbo.RatingTranslations.RatingId AND dbo.Languages.Id = dbo.RatingTranslations.LanguageId
ORDER BY dbo.PlayerRating.Position;
");

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlayerRating",
                columns: table => new
                {
                    PlayersId = table.Column<long>(type: "bigint", nullable: false),
                    RatingId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRating", x => new { x.PlayersId, x.RatingId });
                    table.ForeignKey(
                        name: "FK_PlayerRating_Players_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerRating_Rating_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingTranslations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageId = table.Column<long>(type: "bigint", nullable: false),
                    RatingId = table.Column<long>(type: "bigint", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RatingTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingTranslations_Rating_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRating_RatingId",
                table: "PlayerRating",
                column: "RatingId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingTranslations_LanguageId",
                table: "RatingTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingTranslations_RatingId",
                table: "RatingTranslations",
                column: "RatingId");
        }
    }
}
