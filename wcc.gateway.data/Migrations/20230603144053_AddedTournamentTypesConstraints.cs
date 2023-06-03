using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTournamentTypesConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TournamentTypeId",
                table: "Tournament",
                type: "bigint",
                nullable: false,
                defaultValue: 2L);

            migrationBuilder.CreateIndex(
                name: "IX_Tournament_TournamentTypeId",
                table: "Tournament",
                column: "TournamentTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tournament_TournamentTypes_TournamentTypeId",
                table: "Tournament",
                column: "TournamentTypeId",
                principalTable: "TournamentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tournament_TournamentTypes_TournamentTypeId",
                table: "Tournament");

            migrationBuilder.DropIndex(
                name: "IX_Tournament_TournamentTypeId",
                table: "Tournament");

            migrationBuilder.DropColumn(
                name: "TournamentTypeId",
                table: "Tournament");
        }
    }
}
