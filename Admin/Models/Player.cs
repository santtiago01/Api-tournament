using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("players")]
    public class Player
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("picdni")]
        public byte[]? PicDNI { get; set; }

        [Column("dni")]
        public int DNI { get; set; }

        [Column("birthdate")]
        public DateTime BirthDate { get; set; }

        [Column("firstname")]
        public string? FirstName { get; set; }

        [Column("lastname")]
        public string? LastName { get; set; }

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }
}
