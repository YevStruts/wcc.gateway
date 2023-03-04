using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;

namespace wcc.gateway.data
{
    public interface IDataRepository
    {
        bool AddUser(User user);
        bool UpdateUser(User user);
        User? GetUserByExternalId(string? id);

        Player GetPlayer(long id);
        bool AddPlayer(Player player);
        IEnumerable<Player> GetPlayers();

        News? GetNews(long id);
        IEnumerable<News> GetNewsList();

        Tournament? GetTournament(long id);
        IEnumerable<Tournament> GetTournaments();
    }
}
