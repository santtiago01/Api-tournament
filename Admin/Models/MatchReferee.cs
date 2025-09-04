using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("matchreferees")]
    public class MatchReferee
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("matchid")]
        public long MatchId { get; set; }
        public Match? Match { get; set; }

        [Column("refereeid")]
        public long RefereeId { get; set; }
        public Referee? Referee { get; set; }

        [Column("role")]
        public string Role { get; set; } = string.Empty;
    }
}
