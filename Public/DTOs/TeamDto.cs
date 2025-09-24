namespace TournamentApi.Public.DTOs
{
    public class TeamDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public byte[]? Shield { get; set; }
        public long? CategoryId { get; set; }
    }
}