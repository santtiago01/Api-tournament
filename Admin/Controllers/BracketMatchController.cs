using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class BracketMatchController : ControllerBase
    {
        private readonly AdminDbContext _context;

        public BracketMatchController(AdminDbContext context)
        {
            _context = context;
        }

        [HttpGet("{stageId}")]
        public async Task<IActionResult> GetMatches(int stageId)
        {
            var matches = await _context.bracketmatches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.StageId == stageId)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();
            return Ok(matches);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch(BracketMatch match)
        {
            _context.bracketmatches.Add(match);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMatches), new { stageId = match.StageId }, match);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(int id, BracketMatch match)
        {
            if (id != match.Id) return BadRequest();
            _context.Entry(match).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.bracketmatches.FindAsync(id);
            if (match == null) return NotFound();
            _context.bracketmatches.Remove(match);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
