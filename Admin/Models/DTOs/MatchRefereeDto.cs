namespace TournamentApi.Admin.Models.DTOs
{
    public class MatchRefereeDto
    {
        public long Id { get; set; }
        public RefereeDto? Referee { get; set; }
        public string? Role { get; set; }
    }
}