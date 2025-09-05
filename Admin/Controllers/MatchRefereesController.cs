using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class MatchRefereesController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public MatchRefereesController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetMatchReferees() =>
            Ok(await _context.matchreferees
                             .Include(mr => mr.Match)
                             .Include(mr => mr.Referee)
                             .ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchReferee(long id)
        {
            var matchReferee = await _context.matchreferees
                                             .Include(mr => mr.Match)
                                             .Include(mr => mr.Referee)
                                             .FirstOrDefaultAsync(mr => mr.Id == id);
            if (matchReferee == null) return NotFound();
            return Ok(matchReferee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatchReferee([FromBody] MatchReferee matchReferee)
        {
            _context.matchreferees.Add(matchReferee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMatchReferee), new { id = matchReferee.Id }, matchReferee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatchReferee(long id, [FromBody] MatchReferee matchReferee)
        {
            if (id != matchReferee.Id) return BadRequest();
            _context.Entry(matchReferee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchReferee(long id)
        {
            var matchReferee = await _context.matchreferees.FindAsync(id);
            if (matchReferee == null) return NotFound();
            _context.matchreferees.Remove(matchReferee);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
