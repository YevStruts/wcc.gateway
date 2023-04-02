using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class AddedPLayerRatingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "TotalPoints",
                table: "Rating");

            migrationBuilder.CreateTable(
                name: "PlayerRatingData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    TotalPoints = table.Column<int>(type: "int", nullable: false),
                    PlayerId = table.Column<long>(type: "bigint", nullable: false),
                    RatingId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerRatingData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerRatingData_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlayerRatingData_Rating_RatingId",
                        column: x => x.RatingId,
                        principalTable: "Rating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRatingData_PlayerId",
                table: "PlayerRatingData",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerRatingData_RatingId",
                table: "PlayerRatingData",
                column: "RatingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlayerRatingData");

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Rating",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Progress",
                table: "Rating",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalPoints",
                table: "Rating",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
