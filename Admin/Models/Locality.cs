using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("localities")]
    public class Locality
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }
    }
}
