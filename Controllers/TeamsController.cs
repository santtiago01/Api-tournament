using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Data;
using TournamentApi.Models;

namespace TournamentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeamsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var teams = await _context.teams
                                      .Include(t => t.localities)
                                      .ToListAsync();
            return Ok(teams);
        }

        // GET: api/teams
        public async Task<ActionResult<Team>> GetTeam(long id)
        {
            var team = await _context.teams.Include(t => t.localities)
                                           .FirstOrDefaultAsync(t => t.Id == id);

            if (team == null) return NotFound();

            return team;
        }

        // POST: api/teams
        [HttpPost]
        public async Task<ActionResult<Team>> CreateTeam(Team team)
        {
            team.CreatedAt = DateTime.UtcNow;
            team.UpdatedAt = DateTime.UtcNow;

            _context.teams.Add(team);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
        }

        // PUT: api/teams/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeam(long id, Team team)
        {
            if (id != team.Id) return BadRequest();

            var existingTeam = await _context.teams.FindAsync(id);
            if (existingTeam == null) return NotFound();

            existingTeam.Name = team.Name;
            existingTeam.LocalityId = team.LocalityId;
            existingTeam.Shield = team.Shield;
            existingTeam.State = team.State;
            existingTeam.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/teams/{id}
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
