using Microsoft.EntityFrameworkCore;
using TournamentApi.Public.Models;

namespace TournamentApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Team> teams { get; set; }
        public DbSet<Locality> localities { get; set; }
        public DbSet<Standing> standings { get; set; }
        public DbSet<Tournament> tournaments { get; set; }
        public DbSet<Match> matches { get; set; }
        public DbSet<MatchStatus> matchstatuses { get; set; }
        public DbSet<MatchTeam> matchteams { get; set; }
        public DbSet<Field> fields { get; set; }
        public DbSet<FieldZone> fieldzones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
