using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
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
        public Locality? Locality { get; set; }

        [Column("shield")]
        public byte[]? Shield { get; set; }

        [Column("state")]
        public int State { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }

        // Propiedades de navegación
        public ICollection<PlayerTeam>? PlayerTeams { get; set; }
        public ICollection<MatchTeam>? MatchTeams { get; set; }
    }
}
