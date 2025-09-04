using System.ComponentModel.DataAnnotations.Schema;
using TournamentApi.Models;

namespace TournamentApi.Models
{
    [Table("teams")]
    public class Team
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("localityid")]
        public long LocalityId { get; set; }

        [Column("shield")]
        public byte[]? Shield { get; set; }

        [Column("state")]
        public int State { get; set; }  // 0 = inactivo, 1 = activo

        // Relación
        public Locality? Localities { get; set; }

        public ICollection<MatchTeam>? MatchTeams { get; set; } = new List<MatchTeam>();
        public ICollection<Standing>? Standings { get; set; } = new List<Standing>();
    }
}
