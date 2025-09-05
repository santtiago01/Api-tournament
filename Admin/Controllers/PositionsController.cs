using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class PositionsController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public PositionsController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetPositions() =>
            Ok(await _context.positions.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPosition(long id)
        {
            var position = await _context.positions.FindAsync(id);
            if (position == null) return NotFound();
            return Ok(position);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePosition([FromBody] Position position)
        {
            _context.positions.Add(position);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPosition), new { id = position.Id }, position);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePosition(long id, [FromBody] Position position)
        {
            if (id != position.Id) return BadRequest();
            _context.Entry(position).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePosition(long id)
        {
            var position = await _context.positions.FindAsync(id);
            if (position == null) return NotFound();
            _context.positions.Remove(position);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
