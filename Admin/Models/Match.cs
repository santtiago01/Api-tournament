using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("matches")]
    public class Match
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("tournamentid")]
        public long TournamentId { get; set; }
        public Tournament? Tournament { get; set; }

        [Column("zoneid")]
        public long ZoneId { get; set; }
        public FieldZone? Zone { get; set; }

        [Column("matchdate")]
        public DateTime MatchDate { get; set; }

        [Column("statusid")]
        public long StatusId { get; set; }
        public MatchStatus? Status { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }

        // Propiedades de navegación
        public ICollection<MatchTeam> MatchTeams { get; set; } = new List<MatchTeam>();
        public ICollection<MatchReferee>? MatchReferees { get; set; }
        public ICollection<MatchHistory>? MatchHistory { get; set; }
    }
}