namespace TournamentApi.Admin.Models.DTOs
{
    public class PlayerTeamInfoDto
    {
        public long Id { get; set; }
        public int Dorsal { get; set; }
        public PlayerDto? Player { get; set; }
        public PositionDto? Position { get; set; }
    }
}