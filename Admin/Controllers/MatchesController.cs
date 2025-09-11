using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public MatchesController(AdminDbContext context) => _context = context;

        private bool MatchExists(long id)
        {
            return _context.matches.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetMatches()
        {
            var matches = await _context.matches
                .Include(m => m.Tournament)
                .Include(m => m.Zone)
                .Include(m => m.Status)
                .ToListAsync();

            return Ok(matches);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetMatchesById(long id)
        {
            var match = await _context.matches
                .Include(m => m.Tournament)
                .Include(m => m.Zone)
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null) return NotFound();
            return Ok(match);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] Match match)
        {
            _context.matches.Add(match);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMatchesById), new { id = match.Id }, match);
        }

        [HttpPut("id")]
        public async Task<IActionResult> UpdateMatch(long id, [FromBody] Match match)
        {
            if (id != match.Id) return BadRequest();
            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteMatch(long id)
        {
            var match = await _context.matches.FindAsync(id);
            if (match == null) return NotFound();
            _context.matches.Remove(match);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}