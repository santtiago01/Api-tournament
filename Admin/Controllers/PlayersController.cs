using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public PlayersController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetPlayers() =>
            Ok(await _context.players.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayer(long id)
        {
            var player = await _context.players.FindAsync(id);
            if (player == null) return NotFound();
            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] Player player)
        {
            _context.players.Add(player);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPlayer), new { id = player.Id }, player);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlayer(long id, [FromBody] Player player)
        {
            if (id != player.Id) return BadRequest();
            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(long id)
        {
            var player = await _context.players.FindAsync(id);
            if (player == null) return NotFound();
            _context.players.Remove(player);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
