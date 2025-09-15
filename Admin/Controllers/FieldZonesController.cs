using System.IO.Compression;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("/api/admin/[controller]")]
    [ApiController]
    public class FieldZonesController : ControllerBase
    {
        private readonly AdminDbContext _context;

        public FieldZonesController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> getFieldZones() =>
            Ok(await _context.fieldzones.Include(fz => fz.Field).ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> getFieldZoneById(long id)
        {
            var fieldzone = await _context.fieldzones
                .Include(fz => fz.Field)
                .FirstOrDefaultAsync(fz => fz.Id == id);
            if (fieldzone == null) return NotFound();
            return Ok(fieldzone);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFieldZone([FromBody] FieldZone fieldZone)
        {
            _context.fieldzones.Add(fieldZone);
            await _context.SaveChangesAsync();
            return Ok(fieldZone);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFieldZone(long id, [FromBody] FieldZone fieldZone)
        {
            if (id != fieldZone.Id) return BadRequest();
            _context.Entry(fieldZone).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFieldZone(long id)
        {
            var zonefield = await _context.fieldzones.FindAsync(id);
            if (zonefield == null) return NotFound();
            _context.fieldzones.Remove(zonefield);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}