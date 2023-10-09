using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.Localization;

namespace wcc.gateway.data
{
    public interface IDataRepository
    {
        bool AddUser(User user);
        bool UpdateUser(User user);
        User? GetUserByExternalId(string? id);
        User? GetUserByUsername(string? username);
        Player GetPlayer(long id);
        bool AddPlayer(Player player);
        bool UpdatePlayer(Player player);
        IEnumerable<Player> GetPlayers();

        News? GetNews(long id);
        IEnumerable<News> GetNewsList();

        Tournament? GetTournament(long id);
        IEnumerable<Tournament> GetTournaments();
        bool AddTournamentParticipant(int tournamentId, Player player);
        bool RemoveTournamentParticipant(int tournamentId, Player player);
        
        Language? GetLanguage(string locale);

        Game? GetGame(long id);
        List<Game> GetGames();
        bool UpdateGame(Game game);

        Youtube? GetYoutube(long id);
        List<Youtube> GetYoutubes();
        bool UpdateYoutube(Youtube url);

        Role GetRole(long id);

        List<Role> GetRoles();

        List<LastFightsStatistics> GetLastFightsStatistics(long playerId, int languageId);

        Country GetCountry(long id);

        List<Country> GetCountries();
    }
}
