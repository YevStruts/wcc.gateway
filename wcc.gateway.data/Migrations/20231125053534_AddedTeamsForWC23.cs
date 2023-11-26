using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class AddedTeamsForWC23 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
-- Create touranment
    DECLARE @InsertedIds TABLE (Id INT);

    INSERT INTO [dbo].[Tournament] 
               ([Name], [Description], [ImageUrl], [CountPlayers], [DateStart], [DateCreated], [IsEnrollment], [TournamentTypeId])
    OUTPUT INSERTED.Id INTO @InsertedIds
    VALUES
               ('World Cup 2023 (Qualification)', '', 'https://wcc-cossacks.s3.eu-central-1.amazonaws.com/images/wc23q-logo.png',
                24, '2023-11-01 00:00:00.0000000', '2023-11-01 00:00:00.0000000', 0, 4);

    -- Retrieve the inserted ID
    -- SELECT Id FROM @InsertedIds;

-- Create TouranemntTranslation

    DECLARE @TournamentId INT = (SELECT TOP(1) Id FROM @InsertedIds);

    INSERT INTO [dbo].[TournamentTranslations] ([TournamentId],[LanguageId],[Name],[Description])
    VALUES (@TournamentId, 1, 'World Cup 2023 (Qualification)', '');

    INSERT INTO [dbo].[TournamentTranslations] ([TournamentId],[LanguageId],[Name],[Description])
    VALUES (@TournamentId, 2,N'Кубок Світу 2023 (Кваліфікація)', '');

-- Create teams

    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (  'CPS-2', @TournamentId);
    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (   'Kyiv', @TournamentId);
    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (   'CD-3', @TournamentId);
    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (     'NE', @TournamentId);
    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (  'CPS-3', @TournamentId);
    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES ( 'Spaner', @TournamentId);
    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES ('HAWKS-2', @TournamentId);
    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (  'PKS-2', @TournamentId);
    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (   N'ПЦУ', @TournamentId);
    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES ( 'POLSKA', @TournamentId);
    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (  'CPS-4', @TournamentId);
    INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (     'RT', @TournamentId);

    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (    'CPS', @TournamentId);
    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (   'CD-2', @TournamentId);
    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (    'PKS', @TournamentId);
    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (     'CD', @TournamentId);
    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (  'STENA', @TournamentId);
    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (     'NF', @TournamentId);
    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (   'CD-4', @TournamentId);
    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (  'UNION', @TournamentId);
    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (     'GT', @TournamentId);
    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (  'HAWKS', @TournamentId);
    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (     'WN', @TournamentId);
    -- INSERT INTO [dbo].[Team] ([Name],[TournamentId]) VALUES (    'KGR', @TournamentId);

    -- SELECT @teamId = Id FROM Team WHERE [Name] = 'CPS'
    -- INSERT INTO [dbo].[PlayerTeam] ([PlayersId],[TeamsId]) VALUES ( 15,@teamId);
    -- INSERT INTO [dbo].[PlayerTeam] ([PlayersId],[TeamsId]) VALUES (114,@teamId);
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
