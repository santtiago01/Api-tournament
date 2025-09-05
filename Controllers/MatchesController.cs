using Microsoft.AspNetCore.Mvc;
using TournamentApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TournamentAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly AdminDbContext _context;

        public MatchesController(AdminDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMatches([FromQuery] long? tournamentId)
        {
            var query = _context.matches
                                .Include(m => m.Tournament!)
                                .Include(m => m.Zone!)
                                    .ThenInclude(z => z.Field!)
                                .Include(m => m.Status!)
                                .Include(m => m.MatchTeams!)
                                    .ThenInclude(mt => mt.Team!)
                                .AsQueryable();

            if (tournamentId.HasValue)
                query = query.Where(m => m.TournamentId == tournamentId.Value);

            var matches = await query
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return Ok(matches);
        }
    }
}