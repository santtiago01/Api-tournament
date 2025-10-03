namespace TournamentApi.Admin.Dtos
{
    public class BracketMatchResultDto
    {
        public long Id { get; set; }
        public long MatchId { get; set; }
        public long? WinnerTeamId { get; set; }
        public long? LoserTeamId { get; set; }
        public int WinnerScore { get; set; }
        public int LoserScore { get; set; }
    }
}
