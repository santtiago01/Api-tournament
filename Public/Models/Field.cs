using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Public.Models
{
    [Table("fields")]
    public class Field
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("location")]
        public string Location { get; set; } = string.Empty;

        public ICollection<FieldZone> Zones { get; set; } = new List<FieldZone>();
    }
}