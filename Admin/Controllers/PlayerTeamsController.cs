using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class PlayerTeamsController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public PlayerTeamsController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetPlayerTeams() =>
            Ok(await _context.playerteams
                             .Include(pt => pt.Player)
                             .Include(pt => pt.Team)
                             .Include(pt => pt.Position)
                             .ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerTeam(long id)
        {
            var playerTeam = await _context.playerteams
                                           .Include(pt => pt.Player)
                                           .Include(pt => pt.Team)
                                           .Include(pt => pt.Position)
                                           .FirstOrDefaultAsync(pt => pt.Id == id);
            if (playerTeam == null) return NotFound();
            return Ok(playerTeam);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayerTeam([FromBody] PlayerTeam playerTeam)
        {
            _context.playerteams.Add(playerTeam);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPlayerTeam), new { id = playerTeam.Id }, playerTeam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayerTeam(long id, [FromBody] PlayerTeam playerTeam)
        {
            if (id != playerTeam.Id) return BadRequest();
            _context.Entry(playerTeam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerTeam(long id)
        {
            var playerTeam = await _context.playerteams.FindAsync(id);
            if (playerTeam == null) return NotFound();
            _context.playerteams.Remove(playerTeam);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
