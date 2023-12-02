using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wcc.gateway.data.Migrations
{
    /// <inheritdoc />
    public partial class AddedLastFightStatisticsView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW LastFightsStatistics AS
SELECT
	g.Id as 'GameId',
	g.Name as 'GameName',
	p1.Id as 'PlayerId',
	g.Scheduled as 'Date',
	-- opponent UserId
	CASE
		WHEN hp.Id = p1.Id THEN vp.UserId
		WHEN vp.Id = p1.Id THEN hp.UserId
	END as 'UserId',
	-- opponent Name
	CASE
		WHEN hp.Id = p1.Id THEN vp.[Name]
		WHEN vp.Id = p1.Id THEN hp.[Name]
	END as 'Name',
	tt.[Name] as 'Tournament',
	-- opponent Wins count
	(
		SELECT
			COUNT(Id)
		FROM Games g3 WHERE
		g3.Scheduled < g.Scheduled AND
		CASE
			WHEN HUserId = 
				CASE
					WHEN hp.Id = p1.Id THEN vp.UserId
					WHEN vp.Id = p1.Id THEN hp.UserId
				END
				AND HScore > VScore THEN 1
			WHEN VUserId =
				CASE
					WHEN hp.Id = p1.Id THEN vp.UserId
					WHEN vp.Id = p1.Id THEN hp.UserId
				END
				AND HScore < VScore THEN 1
			ELSE 0
		END = 1
	) as 'Wins',
	-- opponent Losses count
	(
		SELECT
			COUNT(Id)
		FROM Games g3 WHERE
		g3.Scheduled < g.Scheduled AND
		CASE
			WHEN HUserId = 
				CASE
					WHEN hp.Id = p1.Id THEN vp.UserId
					WHEN vp.Id = p1.Id THEN hp.UserId
				END
				AND HScore < VScore THEN 1
			WHEN VUserId = 
				CASE
					WHEN hp.Id = p1.Id THEN vp.UserId
					WHEN vp.Id = p1.Id THEN hp.UserId
				END			
				AND HScore > VScore THEN 1
			ELSE 0
		END = 1
	) as 'Losses',
		(SELECT STRING_AGG(LastFights, ', ') AS ConcatenatedNames FROM
	(
		SELECT TOP(6)
			CASE
				WHEN g2.HUserId = p2.UserId AND g2.HScore > g2.VScore THEN 1
				WHEN g2.HUserId = p2.UserId AND g2.HScore = g2.VScore THEN 0
				WHEN g2.HUserId = p2.UserId AND g2.HScore < g2.VScore THEN -1

				WHEN g2.VUserId = p2.UserId AND g2.VScore > g2.HScore THEN 1
				WHEN g2.VUserId = p2.UserId AND g2.VScore = g2.HScore THEN 0
				WHEN g2.VUserId = p2.UserId AND g2.VScore < g2.HScore THEN -1
			END as LastFights
		FROM Players p2
		JOIN Games g2 ON g2.HUserId = p2.UserId OR g2.VUserId = p2.Userid
		WHERE p2.UserId = 
			CASE
				WHEN hp.Id = p1.Id THEN vp.UserId
				WHEN vp.Id = p1.Id THEN hp.UserId
			END
			AND g2.Scheduled < g.Scheduled
		ORDER BY g2.Scheduled DESC
	) AS Subquery) as 'LastFights',
	CASE
		WHEN hp.Id = p1.Id AND g.HScore > g.VScore THEN 1
		WHEN hp.Id = p1.Id AND g.HScore = g.VScore THEN 0
		WHEN hp.Id = p1.Id AND g.HScore < g.VScore THEN -1
		WHEN vp.Id = p1.Id AND g.VScore > g.HScore THEN 1
		WHEN vp.Id = p1.Id AND g.VScore = g.HScore THEN 0
		WHEN vp.Id = p1.Id AND g.VScore < g.HScore THEN -1
	END as 'Result',
	tt.LanguageId
FROM Games g
LEFT JOIN Tournament t ON t.Id = g.TournamentId
LEFT JOIN TournamentTranslations tt ON tt.TournamentId = t.Id
LEFT JOIN Players p1 ON p1.UserId = g.HUserId OR p1.UserId = g.VUserId
LEFT JOIN Players hp ON hp.UserId = g.HUserId
LEFT JOIN Players vp ON vp.UserId = g.VUserId
WHERE g.GameType = 1
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DROP VIEW LastFightsStatistics;
");
        }
    }
}
