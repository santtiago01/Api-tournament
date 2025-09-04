using System;
using System.ComponentModel.DataAnnotations.Schema;
using TournamentApi.Public.Models;

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

        [Column("stateid")]
        public long StateId { get; set; }
        public MatchStatus? State { get; set; }

        [Column("status")]
        public string Status { get; set; } = string.Empty;

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }
}