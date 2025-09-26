using TournamentApi.Admin.Models.DTOs;

namespace TournamentApi.Public.DTOs
{
    public class TeamDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public byte[]? Shield { get; set; }
        public long? CategoryId { get; set; }

        public CoachDto? Coach { get; set; }
        public List<PlayerTeamInfoDto> Players { get; set; } = new List<PlayerTeamInfoDto>();
    }
}