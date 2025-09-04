using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Models
{
    [Table("fieldzones")]
    public class FieldZone
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("fieldid")]
        public long FieldId { get; set; }
        public Field? Field { get; set; }

        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}