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
        public DbSet<Stage> stages { get; set; }
        public DbSet<BracketMatch> bracketmatches { get; set; }
        public DbSet<BracketMatchResult> bracketmatchresults { get; set; }
        public DbSet<GroupStage> groupstages { get; set; }
        public DbSet<GroupStageTeam> groupstageteams { get; set; }
        public DbSet<GroupStageMatch> groupstagematches { get; set; }
        public DbSet<GroupStageQualifier> groupstagequalifiers { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Match>()
                .HasOne(m => m.Status)
                .WithMany()
                .HasForeignKey(m => m.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación uno a uno Team <-> Coach
            modelBuilder.Entity<Team>()
                .HasOne(t => t.Coach)
                .WithOne(c => c.Team)
                .HasForeignKey<Coach>(c => c.TeamId);
        }
    }
}
