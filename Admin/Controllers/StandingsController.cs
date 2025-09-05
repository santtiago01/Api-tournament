using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public StandingsController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetStandings() =>
            Ok(await _context.standings
                             .Include(s => s.Team)
                             .Include(s => s.Tournament)
                             .ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStanding(long id)
        {
            var standing = await _context.standings
                                         .Include(s => s.Team)
                                         .Include(s => s.Tournament)
                                         .FirstOrDefaultAsync(s => s.Id == id);
            if (standing == null) return NotFound();
            return Ok(standing);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStanding([FromBody] Standing standing)
        {
            _context.standings.Add(standing);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStanding), new { id = standing.Id }, standing);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStanding(long id, [FromBody] Standing standing)
        {
            if (id != standing.Id) return BadRequest();
            _context.Entry(standing).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStanding(long id)
        {
            var standing = await _context.standings.FindAsync(id);
            if (standing == null) return NotFound();
            _context.standings.Remove(standing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
