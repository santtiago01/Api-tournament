using TournamentApi.Admin.Models.DTOs;

namespace TournamentApi.Public.DTOs
{
    public class MatchInfoDto
    {
        public long Id { get; set; }
        public DateTime MatchDate { get; set; }
        public MatchStatusDto? Status { get; set; }
        public TournamentDto? Tournament { get; set; }
        public ZoneDto? Zone { get; set; }
        public FieldDto? Field { get; set; }
        public List<MatchTeamInfoDto> Teams { get; set; } = new List<MatchTeamInfoDto>();
        public List<MatchRefereeInfoDto> Referees { get; set; } = new List<MatchRefereeInfoDto>();
    }
}