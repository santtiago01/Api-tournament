using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Public.Models
{
    [Table("matchstatuses")]
    public class MatchStatus
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}