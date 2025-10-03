using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class StageController : ControllerBase
    {
        private readonly AdminDbContext _context;

        public StageController(AdminDbContext context)
        {
            _context = context;
        }

        [HttpGet("{tournamentId}")]
        public async Task<IActionResult> GetStages(int tournamentId)
        {
            var stages = await _context.stages
                .Where(s => s.TournamentId == tournamentId)
                .OrderBy(s => s.RoundOrder)
                .ToListAsync();
            return Ok(stages);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStage(Stage stage)
        {
            _context.stages.Add(stage);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStages), new { tournamentId = stage.TournamentId }, stage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStage(int id, Stage stage)
        {
            if (id != stage.Id) return BadRequest();
            _context.Entry(stage).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStage(int id)
        {
            var stage = await _context.stages.FindAsync(id);
            if (stage == null) return NotFound();
            _context.stages.Remove(stage);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
