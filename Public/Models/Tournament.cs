using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TournamentApi.Public.Models
{
    [Table("tournaments")]
    public class Tournament
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Column("startdate")]
        public DateTime StartDate { get; set; }

        [Column("finishdate")]
        public DateTime FinishDate { get; set; }

        [Column("state")]
        public int State { get; set; }

        public ICollection<Match> Matches { get; set; } = new List<Match>();
        public ICollection<Standing> Standings { get; set; } = new List<Standing>();
    }
}