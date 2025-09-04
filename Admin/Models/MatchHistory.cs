using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("matchhistory")]
    public class MatchHistory
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("matchid")]
        public long MatchId { get; set; }
        public Match? Match { get; set; }

        [Column("minute")]
        public int Minute { get; set; }

        [Column("playerid")]
        public long? PlayerId { get; set; }
        public Player? Player { get; set; }

        [Column("teamid")]
        public long TeamId { get; set; }
        public Team? Team { get; set; }

        [Column("eventtype")]
        public string EventType { get; set; } = string.Empty;

        [Column("notes")]
        public string? Notes { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }
    }
}
