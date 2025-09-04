using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Models
{
    [Table("standings")]
    public class Standing
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("tournamentid")]
        public long TournamentId { get; set; }
        public Tournament? Tournament { get; set; }

        [Column("teamid")]
        public long TeamId { get; set; }
        public Team? Team { get; set; }

        [Column("played")]
        public int Played { get; set; }

        [Column("wins")]
        public int Wins { get; set; }

        [Column("draws")]
        public int Draws { get; set; }

        [Column("losses")]
        public int Losses { get; set; }

        [Column("goalsfor")]
        public int GoalsFor { get; set; }

        [Column("goalsagainst")]
        public int GoalsAgainst { get; set; }

        [Column("goaldifference")]
        public int GoalDifference { get; set; }

        [Column("points")]
        public int Points { get; set; }
    }
}