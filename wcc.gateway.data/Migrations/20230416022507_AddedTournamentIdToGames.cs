using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTournamentIdToGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TournamentId",
                table: "Games",
                type: "bigint",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.CreateIndex(
                name: "IX_Games_TournamentId",
                table: "Games",
                column: "TournamentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Tournament_TournamentId",
                table: "Games",
                column: "TournamentId",
                principalTable: "Tournament",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Tournament_TournamentId",
                table: "Games");

            migrationBuilder.DropIndex(
                name: "IX_Games_TournamentId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Games");
        }
    }
}
