namespace TournamentApi.Admin.Models.DTOs
{
    public class MatchRefereeInfoDto
    {
        public long Id { get; set; }
        public string? Role { get; set; }
        public RefereeDto? Referee { get; set; }
    }
}