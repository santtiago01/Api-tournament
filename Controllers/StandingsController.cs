using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Data;

namespace TournamentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StandingsController : ControllerBase
    {
        private readonly AdminDbContext _context;

        public StandingsController(AdminDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetStandings([FromQuery] long? tournamentId)
        {
            var query = _context.standings
                .Include(s => s.Team)
                .Include(s => s.Tournament)
                .AsQueryable();

            if (tournamentId.HasValue)
                query = query.Where(s => s.TournamentId == tournamentId.Value);

            var standings = await query
                .OrderByDescending(s => s.Points)
                .ThenByDescending(s => s.GoalDifference)
                .ThenByDescending(s => s.GoalsFor)
                .ToListAsync();

            return Ok(standings);
        }
    }
}