using Microsoft.EntityFrameworkCore;
using TournamentApi.Models;

namespace TournamentApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Team> teams { get; set; }
        public DbSet<Locality> localities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
