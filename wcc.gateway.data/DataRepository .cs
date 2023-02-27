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
            var result = _context.Users.Update(user);
            return result.State == Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        #endregion User

        #region Player

        public bool AddPlayer(Player player)
        {
            _context.Players.Add(player);
            return _context.SaveChanges() == SingleEntry;
        }

        #endregion Player
    }
}
