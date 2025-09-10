using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public TournamentsController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetTournaments() =>
            Ok(await _context.tournaments.ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournament(long id)
        {
            var tournament = await _context.tournaments
                .FirstOrDefaultAsync(t => t.Id == id);
            if (tournament == null) return NotFound();
            return Ok(tournament);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTournament([FromBody] Tournament tournament)
        {
            _context.tournaments.Add(tournament);
            await _context.SaveChangesAsync();
            return Ok(tournament);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTournament(long id, [FromBody] Tournament tournament)
        {
            if (id != tournament.Id) return BadRequest();
            _context.Entry(tournament).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(long id)
        {
            var tournament = await _context.tournaments.FindAsync(id);
            if (tournament == null) return NotFound();
            _context.tournaments.Remove(tournament);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}