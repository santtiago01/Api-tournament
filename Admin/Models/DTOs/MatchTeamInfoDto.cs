using TournamentApi.Public.DTOs;

namespace TournamentApi.Admin.Models.DTOs
{
    public class MatchTeamInfoDto
    {
        public long Id { get; set; }
        public int Score { get; set; }
        public TeamDto? Team { get; set; }
        public CoachDto? Coach { get; set; }
        public List<PlayerTeamInfoDto> Players { get; set; } = new List<PlayerTeamInfoDto>();
    }
}