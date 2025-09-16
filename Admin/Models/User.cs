using System.ComponentModel.DataAnnotations.Schema;

namespace TournamentApi.Admin.Models
{
    [Table("users")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; } = string.Empty;

        [Column("passwordhash")]
        public string Password { get; set; } = string.Empty; // luego se encripta con hash

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("role")]
        public string Role { get; set; } = "Admin"; // Admin, User, etc.

        [Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
