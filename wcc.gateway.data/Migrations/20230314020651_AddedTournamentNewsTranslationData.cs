using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTournamentNewsTranslationData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO dbo.Languages ([Name] ,[Culture]) VALUES ('English', 'gb');
INSERT INTO dbo.Languages ([Name] ,[Culture]) VALUES ('Ukrainian', 'uk');
");

            /* Tournament Table */
            migrationBuilder.Sql(@"
DECLARE @CursorTestID INT = 1;
DECLARE @RunningTotal BIGINT = 0;
DECLARE @RowCnt BIGINT = 0;

-- get a count of total rows to process 
SELECT @RowCnt = COUNT(0) FROM dbo.Tournament;
 
WHILE @CursorTestID <= @RowCnt
BEGIN
   DECLARE @GBLanguageId INT = 1;
   SELECT @GBLanguageId = Id FROM dbo.Languages WHERE Culture = 'gb';

   DECLARE @UKLanguageId INT = 2;
   SELECT @UKLanguageId = Id FROM dbo.Languages WHERE Culture = 'uk';

   INSERT INTO dbo.TournamentTranslations SELECT [id], @GBLanguageId, 'TBD', 'TBD' FROM dbo.Tournament WHERE Id = @CursorTestID;
   INSERT INTO dbo.TournamentTranslations SELECT [id], @UKLanguageId, [Name], [Description] FROM dbo.Tournament WHERE Id = @CursorTestID;

   SET @RunningTotal += @CursorTestID
   SET @CursorTestID = @CursorTestID + 1 
END;
");

            /* News Table */
            migrationBuilder.Sql(@"
DECLARE @CursorTestID INT = 1;
DECLARE @RunningTotal BIGINT = 0;
DECLARE @RowCnt BIGINT = 0;

-- get a count of total rows to process 
SELECT @RowCnt = COUNT(0) FROM dbo.News;
 
WHILE @CursorTestID <= @RowCnt
BEGIN
   DECLARE @GBLanguageId INT = 1;
   SELECT @GBLanguageId = Id FROM dbo.Languages WHERE Culture = 'gb';

   DECLARE @UKLanguageId INT = 2;
   SELECT @UKLanguageId = Id FROM dbo.Languages WHERE Culture = 'uk';

   INSERT INTO dbo.NewsTranslations SELECT [id], @GBLanguageId, 'TBD', 'TBD' FROM dbo.News WHERE Id = @CursorTestID;
   INSERT INTO dbo.NewsTranslations SELECT [id], @UKLanguageId, [Name], [Description] FROM dbo.News WHERE Id = @CursorTestID;

   SET @RunningTotal += @CursorTestID
   SET @CursorTestID = @CursorTestID + 1 
END;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
