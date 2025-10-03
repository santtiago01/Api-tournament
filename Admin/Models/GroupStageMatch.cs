using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("group_stage_matches")]
    public class GroupStageMatch
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("group_stage_id")]
        public long GroupStageId { get; set; }

        [ForeignKey(nameof(GroupStageId))]
        public GroupStage? GroupStage { get; set; }

        [Column("home_team_id")]
        public long HomeTeamId { get; set; }

        [ForeignKey(nameof(HomeTeamId))]
        public Team? HomeTeam { get; set; }

        [Column("away_team_id")]
        public long AwayTeamId { get; set; }

        [ForeignKey(nameof(AwayTeamId))]
        public Team? AwayTeam { get; set; }

        [Column("match_date")]
        public DateTime MatchDate { get; set; }

        [Column("home_goals")]
        public int? HomeGoals { get; set; }

        [Column("away_goals")]
        public int? AwayGoals { get; set; }

        // Estado: "Pendiente", "En Juego", "Finalizado"
        [Column("status")]
        public string Status { get; set; } = "Pendiente";
    }
}