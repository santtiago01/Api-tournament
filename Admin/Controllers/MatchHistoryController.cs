using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class MatchHistoryController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public MatchHistoryController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetMatchHistory() =>
            Ok(await _context.matchhistories
                             .Include(mh => mh.Match)
                             .Include(mh => mh.Player)
                             .Include(mh => mh.Team)
                             .ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchEvent(long id)
        {
            var matchEvent = await _context.matchhistories
                                           .Include(mh => mh.Match)
                                           .Include(mh => mh.Player)
                                           .Include(mh => mh.Team)
                                           .FirstOrDefaultAsync(mh => mh.Id == id);
            if (matchEvent == null) return NotFound();
            return Ok(matchEvent);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatchEvent([FromBody] MatchHistory matchEvent)
        {
            _context.matchhistories.Add(matchEvent);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMatchEvent), new { id = matchEvent.Id }, matchEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatchEvent(long id, [FromBody] MatchHistory matchEvent)
        {
            if (id != matchEvent.Id) return BadRequest();
            _context.Entry(matchEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchEvent(long id)
        {
            var matchEvent = await _context.matchhistories.FindAsync(id);
            if (matchEvent == null) return NotFound();
            _context.matchhistories.Remove(matchEvent);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
