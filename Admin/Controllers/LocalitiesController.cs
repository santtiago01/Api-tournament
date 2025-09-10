using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class LocalitiesController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public LocalitiesController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetLocalities() =>
            Ok(await _context.localities.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocality(long id)
        {
            var locality = await _context.localities.FindAsync(id);
            if (locality == null) return NotFound();
            return Ok(locality);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocality([FromBody] Locality locality)
        {
            _context.localities.Add(locality);
            await _context.SaveChangesAsync();
            return Ok(locality);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLocality(long id, [FromBody] Locality locality)
        {
            if (id != locality.Id) return BadRequest();
            _context.Entry(locality).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLocality(long id)
        {
            var locality = await _context.localities.FindAsync(id);
            if (locality == null) return NotFound();
            _context.localities.Remove(locality);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}