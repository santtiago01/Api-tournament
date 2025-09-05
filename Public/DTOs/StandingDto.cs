namespace TournamentApi.Public.DTOs
{
    public class StandingDto
    {
        public long Id { get; set; }
        public long Points { get; set; }
        public long GoalsFor { get; set; }
        public long GoalDifference { get; set; }
        public TeamDto? Team { get; set; }
    }
}