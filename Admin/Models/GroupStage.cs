using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("group_stages")]
    public class GroupStage
    {
        [Column("id")]
        public long Id { get; set; }

        // FK al torneo
        [Column("tournament_id")]
        public long TournamentId { get; set; }

        [ForeignKey(nameof(TournamentId))]
        public Tournament? Tournament { get; set; }

        // Ejemplo: "Grupo A", "Grupo B"
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("teams_to_qualify")]
        public int TeamsToQualify { get; set; } = 2;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        // Relación con equipos
        public ICollection<GroupStageTeam> Teams { get; set; } = new List<GroupStageTeam>();
    }
}