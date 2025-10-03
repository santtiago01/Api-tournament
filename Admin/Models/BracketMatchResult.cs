using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("bracket_match_results")]
    public class BracketMatchResult
    {
        [Column("id")]
        public long Id { get; set; }   // bigint, no int

        [Column("bracket_match_id")]
        public long BracketMatchId { get; set; }   // bigint

        [ForeignKey(nameof(BracketMatchId))]
        public BracketMatch? BracketMatch { get; set; }

        [Column("home_goals")]
        public int HomeGoals { get; set; }

        [Column("away_goals")]
        public int AwayGoals { get; set; }

        [Column("winner_team_id")]
        public long? WinnerTeamId { get; set; }

        [ForeignKey(nameof(WinnerTeamId))]
        public Team? WinnerTeam { get; set; }

        [Column("recorded_at")]
        public DateTime RecordedAt { get; set; }
    }
}