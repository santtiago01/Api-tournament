using System.ComponentModel.DataAnnotations.Schema;
using TournamentApi.Models;

namespace TournamentApi.Models
{
    [Table("teams")]
    public class Team
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("localityid")]
        public long LocalityId { get; set; }

        [Column("shield")]
        public byte[]? Shield { get; set; }

        [Column("state")]
        public int State { get; set; }  // 0 = inactivo, 1 = activo

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }

        // Relación
        public Locality? localities { get; set; }
    }
}
