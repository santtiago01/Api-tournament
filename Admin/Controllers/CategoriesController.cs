using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public CategoriesController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetCategories() =>
            Ok(await _context.categories.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(long id)
        {
            var category = await _context.categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] Category category)
        {
            _context.categories.Add(category);
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(long id, [FromBody] Category category)
        {
            if (id != category.Id) return BadRequest();
            _context.Entry(category).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var category = await _context.categories.FindAsync(id);
            if (category == null) return NotFound();
            _context.categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
