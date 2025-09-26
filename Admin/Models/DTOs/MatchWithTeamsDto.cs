namespace TournamentApi.Admin.Models.DTOs
{
    public class MatchWithTeamsDto
    {
        public long MatchId { get; set; }
        public long HomeTeamId { get; set; }
        public string? HomeTeamName { get; set; }
        public long AwayTeamId { get; set; }
        public string? AwayTeamName { get; set; }
        public DateTime MatchDate { get; set; }
    }
}