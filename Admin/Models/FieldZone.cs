using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("fieldzones")]
    public class FieldZone
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("fieldid")]
        public long FieldId { get; set; }
        public Field? Field { get; set; }

        [Column("name")]
        public string? Name { get; set; }
    }
}
