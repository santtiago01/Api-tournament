using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class MatchStatusesController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public MatchStatusesController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetMatchStatuses() =>
            Ok(await _context.matchstatuses.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchStatusById(long id)
        {
            var matchStatus = await _context.matchstatuses.FirstOrDefaultAsync(ms => ms.Id == id);
            if (matchStatus == null) return NotFound();
            return Ok(matchStatus);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatchStatus([FromBody] MatchStatus matchStatus)
        {
            _context.matchstatuses.Add(matchStatus);
            await _context.SaveChangesAsync();
            return Ok(matchStatus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatchStatus(long id, [FromBody] MatchStatus matchStatus)
        {
            if (id != matchStatus.Id) return BadRequest();
            _context.Entry(matchStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchStatus(long id)
        {
            var matchStatus = await _context.matchstatuses.FindAsync(id);
            if (matchStatus == null) return NotFound();
            _context.matchstatuses.Remove(matchStatus);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}