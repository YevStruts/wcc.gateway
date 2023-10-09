using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNationForExistingPlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
UPDATE Players SET [CountryId] = 253 WHERE Id IN (41,28,44,97,94,74,101);
UPDATE Players SET [CountryId] = 177 WHERE Id IN (45,20,24,102,47,50,52,
19,51,53,29,40,65,56,116,152,135,158,121,133,148,155,134,149,143,156,144,
136,151,150,154,141);
UPDATE Players SET [CountryId] = 236 WHERE Id IN (39,38);
UPDATE Players SET [CountryId] = 65 WHERE Id IN (60);
UPDATE Players SET [CountryId] = 234 WHERE Id IN (91);
UPDATE Players SET [CountryId] = 28 WHERE Id IN (93);
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
