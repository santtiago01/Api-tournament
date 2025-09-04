using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Public.Models
{
    [Table("matchteams")]
    public class MatchTeam
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("matchid")]
        public long MatchId { get; set; }

        [Column("teamid")]
        public long TeamId { get; set; }
        public Team? Team { get; set; }

        [Column("ishome")]
        public bool IsHome { get; set; }

        [Column("goals")]
        public int Goals { get; set; }
    }
}