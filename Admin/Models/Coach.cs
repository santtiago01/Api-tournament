using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("coaches")]
    public class Coach
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("firstname")]
        public string? FirstName { get; set; }

        [Column("lastname")]
        public string? LastName { get; set; }

        [Column("dni")]
        public string? DNI { get; set; }

        [Column("birthdate")]
        public DateTime BirthDate { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("picdni")]
        public byte[]? PicDNI { get; set; }

        [Column("teamid")]
        public long TeamId { get; set; }
        public Team? Team { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }
}
