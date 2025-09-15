namespace TournamentApi.Public.DTOs
{
    public class MatchHistoryDto
    {
        public int Minute { get; set; }
        public string EventType { get; set; } = "";
        public string? PlayerName { get; set; }
        public TeamDto? Team { get; set; }
    }
}