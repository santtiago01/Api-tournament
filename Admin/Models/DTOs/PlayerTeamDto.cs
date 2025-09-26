namespace TournamentApi.Admin.Models.DTOs
{
    public class PlayerTeamDto
    {
        public long Id { get; set; }
        public PlayerDto? Player { get; set; }
        public int Dorsal { get; set; }
        public PositionDto? Position { get; set; }
    }
}