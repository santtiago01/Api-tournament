using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("playerteams")]
    public class PlayerTeam
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("playerid")]
        public long PlayerId { get; set; }
        public Player? Player { get; set; }

        [Column("teamid")]
        public long TeamId { get; set; }
        public Team? Team { get; set; }

        [Column("dorsal")]
        public int Dorsal { get; set; }

        [Column("positionid")]
        public long PositionId { get; set; }
        public Position? Position { get; set; }

        [Column("startdate")]
        public DateTime StartDate { get; set; }

        [Column("enddate")]
        public DateTime? EndDate { get; set; }
    }
}
