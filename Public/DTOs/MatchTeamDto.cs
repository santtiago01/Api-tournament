namespace TournamentApi.Public.DTOs
{
    public class MatchTeamDto
    {
        public long Id { get; set; }
        public long TeamId { get; set; }
        public TeamDto? Team { get; set; }
        public int Score { get; set; }
    }
}