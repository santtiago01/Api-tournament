using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("bracket_matches")]
    public class BracketMatch
    {
        [Column("id")]
        public long Id { get; set; }   // <-- CAMBIAR a long

        [Column("stage_id")]
        public long StageId { get; set; }  // <-- bigint también
        [ForeignKey(nameof(StageId))]
        public Stage? Stage { get; set; }

        [Column("home_team_id")]
        public long HomeTeamId { get; set; }
        [ForeignKey(nameof(HomeTeamId))]
        public Team? HomeTeam { get; set; }

        [Column("away_team_id")]
        public long AwayTeamId { get; set; }
        [ForeignKey(nameof(AwayTeamId))]
        public Team? AwayTeam { get; set; }

        [Column("home_goals")]
        public int? HomeGoals { get; set; }

        [Column("away_goals")]
        public int? AwayGoals { get; set; }

        [Column("winner_team_id")]
        public long? WinnerTeamId { get; set; }
        [ForeignKey(nameof(WinnerTeamId))]
        public Team? WinnerTeam { get; set; }

        [Column("loser_team_id")]
        public long? LoserTeamId { get; set; }
        [ForeignKey(nameof(LoserTeamId))]
        public Team? LoserTeam { get; set; }

        [Column("status")]
        public string? Status { get; set; }

        [Column("match_date")]
        public DateTime MatchDate { get; set; }
    }
}