using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCountryIdFix1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "Players",
                type: "bigint",
                nullable: false,
                defaultValue: 232);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn("CountryId", "Players");
        }
    }
}
