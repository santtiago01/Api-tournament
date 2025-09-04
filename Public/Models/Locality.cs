using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TournamentApi.Public.Models
{
    [Table("localities")]
    public class Locality
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [JsonIgnore]
        public ICollection<Team>? Teams { get; set; } = new List<Team>();
    }
}