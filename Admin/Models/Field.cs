using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("fields")]
    public class Field
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("location")]
        public string? Location { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }
}
