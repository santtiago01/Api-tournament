using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("group_stage_teams")]
    public class GroupStageTeam
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("group_stage_id")]
        public long GroupStageId { get; set; }

        [ForeignKey(nameof(GroupStageId))]
        public GroupStage? GroupStage { get; set; }

        [Column("team_id")]
        public long TeamId { get; set; }

        [ForeignKey(nameof(TeamId))]
        public Team? Team { get; set; }

        // Estadísticas acumuladas
        [Column("points")]
        public int Points { get; set; } = 0;

        [Column("played")]
        public int Played { get; set; } = 0;

        [Column("wins")]
        public int Wins { get; set; } = 0;

        [Column("draws")]
        public int Draws { get; set; } = 0;

        [Column("losses")]
        public int Losses { get; set; } = 0;

        [Column("goals_for")]
        public int GoalsFor { get; set; } = 0;
        
        [Column("goals_against")]
        public int GoalsAgainst { get; set; } = 0;

        [Column("goal_difference")]
        public int GoalDifference => GoalsFor - GoalsAgainst;
    }
}