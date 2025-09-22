namespace TournamentApi.Public.DTOs
{
    public class TournamentDto
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}