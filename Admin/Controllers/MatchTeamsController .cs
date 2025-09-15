using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class MatchTeamsController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public MatchTeamsController(AdminDbContext context) => _context = context;

        // GET: api/admin/matchteams
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matchTeams = await _context.matchteams
                .Include(mt => mt.Match)
                .Include(mt => mt.Team)
                .ToListAsync();

            return Ok(matchTeams);
        }

        // GET: api/admin/matchteams/by-match/10
        [HttpGet("by-match/{matchId}")]
        public async Task<IActionResult> GetByMatch(long matchId)
        {
            var matchTeams = await _context.matchteams
                .Include(mt => mt.Team)
                .Where(mt => mt.MatchId == matchId)
                .ToListAsync();

            return Ok(matchTeams);
        }

        // POST: api/admin/matchteams
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MatchTeam matchTeam)
        {
            _context.matchteams.Add(matchTeam);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = matchTeam.Id }, matchTeam);
        }

        // PUT: api/admin/matchteams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] MatchTeam matchTeam)
        {
            if (id != matchTeam.Id) return BadRequest();
            _context.Entry(matchTeam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/admin/matchteams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var matchTeam = await _context.matchteams.FindAsync(id);
            if (matchTeam == null) return NotFound();
            _context.matchteams.Remove(matchTeam);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
