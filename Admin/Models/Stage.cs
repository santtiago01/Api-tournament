using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("stages")]
    public class Stage
    {
        [Column("id")]
        public long Id { get; set; }

        // FK al torneo
        [Column("tournament_id")]
        public long TournamentId { get; set; }

        [ForeignKey(nameof(TournamentId))]
        public Tournament? Tournament { get; set; }

        // Nombre de la fase: "8vos", "4tos", "Semifinal", "Final", "Tercer Puesto"
        [Column("name")]
        public string? Name { get; set; }

        // Orden de la fase para mostrar: 1=8vos, 2=4tos, etc.
        [Column("round_order")]
        public int RoundOrder { get; set; }

        // Relación con los partidos de esta fase
        public ICollection<BracketMatch> Matches { get; set; } = new List<BracketMatch>();
        public ICollection<GroupStage> GroupStages { get; set; } = new List<GroupStage>();
    }
}