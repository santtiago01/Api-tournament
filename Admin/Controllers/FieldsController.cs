using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("/api/admin/[controller]")]
    [ApiController]
    public class FieldsController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public FieldsController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetFields() =>
            Ok(await _context.fields.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFieldById(long id)
        {
            var field = await _context.fields.FirstOrDefaultAsync(f => f.Id == id);
            if (field == null) return NotFound();
            return Ok(field);
        }

        [HttpPost]
        public async Task<IActionResult> CreateField([FromBody] Field field)
        {
            _context.fields.Add(field);
            await _context.SaveChangesAsync();
            return Ok(field);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateField(long id, [FromBody] Field field)
        {
            if (id != field.Id) return BadRequest();
            _context.Entry(field).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteField(long id)
        {
            var field = await _context.fields.FindAsync(id);
            if (field == null) return NotFound();
            _context.fields.Remove(field);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}