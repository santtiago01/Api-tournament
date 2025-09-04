using System.ComponentModel.DataAnnotations.Schema;
using TournamentApi.Models;
using System.Text.Json.Serialization;

namespace TournamentApi.Models
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