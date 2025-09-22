using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public TeamsController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetTeams() =>
            Ok(await _context.teams
                .Include(t => t.Locality)
                .Include(t => t.Category)
                .ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeam(long id)
        {
            var team = await _context.teams.Include(t => t.Locality)
                                           .Include(t => t.Category)
                                           .FirstOrDefaultAsync(t => t.Id == id);
            if (team == null) return NotFound();
            return Ok(team);
        }

        [HttpGet("by-category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamsByCategory(long categoryId)
        {
            var teams = await _context.teams
                .Where(t => t.CategoryId == categoryId)
                .ToListAsync();

            if (teams == null || teams.Count == 0)
            {
                return NotFound();
            }

            return Ok(teams);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] Team team)
        {
            _context.teams.Add(team);
            await _context.SaveChangesAsync();
            return Ok(team);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(long id, [FromBody] Team team)
        {
            if (id != team.Id) return BadRequest();
            _context.Entry(team).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(long id)
        {
            var team = await _context.teams.FindAsync(id);
            if (team == null) return NotFound();
            _context.teams.Remove(team);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
