using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;

namespace wcc.gateway.data
{
    public class DataRepository : IDataRepository
    {
        private readonly ApplicationDbContext _context;

        private const int SingleEntry = 1;

        public DataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region User

        public bool AddUser(User user)
        {
            _context.Users.Add(user);
            return _context.SaveChanges() == SingleEntry;
        }

        public User? GetUserByExternalId(string? id)
        {
            if (string.IsNullOrEmpty(id)) return null;
            return _context.Users.FirstOrDefault(u => u.ExternalId == id);
        }

        public bool UpdateUser(User user)
        {
            // TODO: Check what returns.
            _context.Users.Update(user);
            return _context.SaveChanges() == SingleEntry;
        }

        #endregion User

        #region Player

        public Player GetPlayer(long id)
        {
            throw new NotImplementedException();
        }
        
        public IEnumerable<Player> GetPlayers()
        {
            return _context.Players.ToList();
        }

        public bool AddPlayer(Player player)
        {
            _context.Players.Add(player);
            return _context.SaveChanges() == SingleEntry;
        }

        #endregion Player

        #region News

        public News? GetNews(long id)
        {
            return _context.News.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<News> GetNewsList()
        {
            return _context.News.ToList();
        }

        #endregion News

        #region Tournament

        public Tournament? GetTournament(long id)
        {
            return _context.Tournaments.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Tournament> GetTournaments()
        {
            return _context.Tournaments.ToList();
        }

        #endregion Tournament
    }
}
