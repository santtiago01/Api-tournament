namespace TournamentApi.Admin.Models
{
    public class GroupStageCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public long TournamentId { get; set; }
        public int TeamsToQualify { get; set; } = 2;
    }
}
