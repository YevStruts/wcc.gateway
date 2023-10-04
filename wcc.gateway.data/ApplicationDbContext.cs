using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using wcc.gateway.Identity;
using wcc.gateway.Infrastructure;
using wcc.gateway.Localization;

namespace wcc.gateway.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsTranslations> NewsTranslations { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<TournamentType> TournamentTypes { get; set; }
        public virtual DbSet<TournamentTranslations> TournamentsTranslations { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Youtube> YoutubeUrls { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<LastFightsStatistics> LastFightsStatistics { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LastFightsStatistics>().ToView("LastFightsStatistics");
        }
    }
}