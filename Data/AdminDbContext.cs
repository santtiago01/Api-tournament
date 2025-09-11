using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;

namespace TournamentApi.Data
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext(DbContextOptions<AdminDbContext> options) : base(options) { }

        public DbSet<Category> categories { get; set; }
        public DbSet<Coach> coaches { get; set; }
        public DbSet<Field> fields { get; set; }
        public DbSet<FieldZone> fieldzones { get; set; }
        public DbSet<Locality> localities { get; set; }
        public DbSet<Match> matches { get; set; }
        public DbSet<MatchHistory> matchhistories { get; set; }
        public DbSet<MatchReferee> matchreferees { get; set; }
        public DbSet<MatchStatus> matchstatuses { get; set; }
        public DbSet<MatchTeam> matchteams { get; set; }
        public DbSet<Player> players { get; set; }
        public DbSet<PlayerTeam> playerteams { get; set; }
        public DbSet<Position> positions { get; set; }
        public DbSet<Referee> referees { get; set; }
        public DbSet<Standing> standings { get; set; }
        public DbSet<Team> teams { get; set; }
        public DbSet<Tournament> tournaments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Status)
                .WithMany()
                .HasForeignKey(m => m.StatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
