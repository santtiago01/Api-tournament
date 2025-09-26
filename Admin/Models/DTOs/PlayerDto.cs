namespace TournamentApi.Admin.Models.DTOs
{
    public class PlayerDto
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int DNI { get; set; }
        public byte[]? PicDNI { get; set; }
    }
}