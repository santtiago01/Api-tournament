using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Models
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
        public int StatusId { get; set; }
        public MatchStatus? Status { get; set; }

        public ICollection<MatchTeam>? MatchTeams { get; set; } = new List<MatchTeam>();
    }
}