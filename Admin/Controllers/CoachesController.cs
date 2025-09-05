using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CoachesController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public CoachesController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetCoaches() =>
            Ok(await _context.coaches.Include(c => c.Team).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoach(long id)
        {
            var coach = await _context.coaches.Include(c => c.Team)
                                              .FirstOrDefaultAsync(c => c.Id == id);
            if (coach == null) return NotFound();
            return Ok(coach);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoach([FromBody] Coach coach)
        {
            _context.coaches.Add(coach);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCoach), new { id = coach.Id }, coach);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCoach(long id, [FromBody] Coach coach)
        {
            if (id != coach.Id) return BadRequest();
            _context.Entry(coach).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoach(long id)
        {
            var coach = await _context.coaches.FindAsync(id);
            if (coach == null) return NotFound();
            _context.coaches.Remove(coach);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
