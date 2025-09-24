namespace TournamentApi.Public.DTOs
{
    public class StandingDto
    {
        public long TeamId { get; set; }
        public string? TeamName { get; set; }
        public string? TeamLogo { get; set; }
        public long? CategoryId { get; set; }
        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
    }
}