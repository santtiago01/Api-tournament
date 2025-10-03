using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("group_stage_qualifiers")]
    public class GroupStageQualifier
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("group_stage_id")]
        public int GroupStageId { get; set; }
        public GroupStage? GroupStage { get; set; }

        [Column("stage_id")]
        public int StageId { get; set; }
        public Stage? Stage { get; set; }

        [Column("position")]
        public int Position { get; set; } // 1=Primero, 2=Segundo, etc.

        [Column("bracket_match_id")]
        public int BracketMatchId { get; set; }
        public BracketMatch? BracketMatch { get; set; }

        [Column("slot")]
        public string Slot { get; set; } = "home"; // "home" o "away"
    }
}