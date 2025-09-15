namespace TournamentApi.Public.DTOs
{
    public class MatchDto
    {
        public long Id { get; set; }
        public DateTime MatchDate { get; set; }
        public MatchStatusDto? Status { get; set; }
        public TournamentDto? Tournament { get; set; }
        public ZoneDto? Zone { get; set; }
        public IEnumerable<MatchTeamDto>? Teams { get; set; }
    }
}