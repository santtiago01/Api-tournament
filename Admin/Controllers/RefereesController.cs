using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class RefereesController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public RefereesController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetReferees() =>
            Ok(await _context.referees.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReferee(long id)
        {
            var referee = await _context.referees.FindAsync(id);
            if (referee == null) return NotFound();
            return Ok(referee);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReferee([FromBody] Referee referee)
        {
            _context.referees.Add(referee);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReferee), new { id = referee.Id }, referee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReferee(long id, [FromBody] Referee referee)
        {
            if (id != referee.Id) return BadRequest();
            _context.Entry(referee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReferee(long id)
        {
            var referee = await _context.referees.FindAsync(id);
            if (referee == null) return NotFound();
            _context.referees.Remove(referee);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
