using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("referees")]
    public class Referee
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("picdni")]
        public byte[]? PicDNI { get; set; }

        [Column("dni")]
        public int DNI { get; set; }

        [MaxLength(25)]
        [Column("firstname")]
        public string? FirstName { get; set; }

        [MaxLength(25)]
        [Column("lastname")]
        public string? LastName { get; set; }

        [Column("birthdate")]
        public DateTime BirthDate { get; set; }

        [Column("state")]
        public int State { get; set; } // 0 = Inactivo, 1 = Activo

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }

        // Propiedad de navegación
        public ICollection<MatchReferee>? MatchReferees { get; set; }
    }
}
