using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("tournaments")]
    public class Tournament
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("startdate")]
        public DateTime StartDate { get; set; }

        [Column("finishdate")]
        public DateTime FinishDate { get; set; }

        [Column("state")]
        public int State { get; set; }

        [Column("iscurrent")]
        public bool IsCurrent { get; set; } = false;

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }
}
