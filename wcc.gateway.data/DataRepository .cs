using Microsoft.EntityFrameworkCore;
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
            return _context.Users.Include(p => p.Player).Include(r => r.Role).FirstOrDefault(u => u.ExternalId == id);
        }

        public bool UpdateUser(User user)
        {
            // TODO: Check what returns.
            _context.Users.Update(user);
            return _context.SaveChanges() == SingleEntry;
        }

        #endregion User

        #region Player

        public Player? GetPlayer(long id)
        {
            return _context.Players.FirstOrDefault(p => p.Id == id);
        }
        
        public IEnumerable<Player> GetPlayers()
        {
            return _context.Players.Include(p => p.User).Include(p => p.Tournament).ToList();
        }

        public bool AddPlayer(Player player)
        {
            _context.Players.Add(player);
            return _context.SaveChanges() == SingleEntry;
        }

        public bool UpdatePlayer(Player player)
        {
            _context.Players.Update(player);
            return _context.SaveChanges() == SingleEntry;
        }
        #endregion Player

        #region News

        public News? GetNews(long id)
        {
            return _context.News.Include(t => t.Translations).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<News> GetNewsList()
        {
            return _context.News.Include(t => t.Translations).ToList();
        }

        #endregion News

        #region Tournament

        public Tournament? GetTournament(long id)
        {
            return _context.Tournaments.Include(p => p.Participant).Include(l => l.Translations).FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Tournament> GetTournaments()
        {
            return _context.Tournaments.Include(l => l.Translations).ToList();
        }

        public bool AddTournamentParticipant(int tournamentId, Player player)
        {
            var tournament = GetTournament(tournamentId);
            if (tournament != null && !tournament.Participant.Any(p => p.Id == player.Id))
            {
                tournament.Participant.Add(player);
                return _context.SaveChanges() == SingleEntry;
            }
            return false;
        }

        public bool RemoveTournamentParticipant(int tournamentId, Player player)
        {
            var tournament = GetTournament(tournamentId);
            if (tournament != null && tournament.Participant.Any(p => p.Id == player.Id))
            {
                tournament.Participant.Remove(player);
                return _context.SaveChanges() == SingleEntry;
            }
            return false;
        }

        #endregion Tournament

        #region Language
        public Language? GetLanguage(string culture)
        {
            return _context.Languages.FirstOrDefault(u => u.Culture == culture);
        }
        #endregion Language

        #region Game
        public Game? GetGame(long id)
        {
            return _context.Games.Include(g => g.YoutubeUrls).FirstOrDefault(u => u.Id == id);
        }

        public List<Game> GetGames()
        {
            return _context.Games.ToList();
        }

        public bool UpdateGame(Game game)
        {
            _context.Games.Update(game);
            return _context.SaveChanges() == SingleEntry;
        }
        #endregion Game

        #region Youtube
        public Youtube? GetYoutube(long id)
        {
            return _context.YoutubeUrls.FirstOrDefault(u => u.Id == id);
        }

        public List<Youtube> GetYoutubes()
        {
            return _context.YoutubeUrls.ToList();
        }

        public bool UpdateYoutube(Youtube url)
        {
            _context.YoutubeUrls.Update(url);
            return _context.SaveChanges() == SingleEntry;
        }
        #endregion Youtube

        #region Role
        public Role GetRole(long id)
        {
            return _context.Roles.First(r => r.Id == id);
        }
        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
        #endregion
    }
}
