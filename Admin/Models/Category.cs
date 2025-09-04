using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("categories")]
    public class Category
    {
        [Column("id")]
        public long Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("state")]
        public int State { get; set; } // 0 = Inactivo, 1 = Activo

        [Column("createdat")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }
}
