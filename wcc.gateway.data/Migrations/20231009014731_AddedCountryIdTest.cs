using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCountryIdTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Players_CountryId",
                table: "Players",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Country_CountryId",
                table: "Players",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Country_CountryId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_CountryId",
                table: "Players");
        }
    }
}
