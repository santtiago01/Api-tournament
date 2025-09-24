using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;
using TournamentApi.Public.DTOs;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/public/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly AdminDbContext _context;
        public StandingsController(AdminDbContext context) => _context = context;

        [HttpGet]
        public async Task<IActionResult> GetStandings() =>
            Ok(await _context.standings
                             .Include(s => s.Team)
                             .Include(s => s.Tournament)
                             .ToListAsync());

        [HttpGet("{tournamentId}")]
        public async Task<IActionResult> GetStanding(long tournamentId)
        {
            var matches = await _context.matches
                .Include(m => m.MatchTeams!).ThenInclude(mt => mt.Team)
                .Include(m => m.Status)
                .Where(m => m.TournamentId == tournamentId)
                .ToListAsync();

            if (!matches.Any())
                return NotFound("No se encontraron partidos para este torneo.");

            var finishedMatches = matches
                .Where(m => m.Status != null && m.Status.Name != null && m.Status.Name.Equals("Finalizado", StringComparison.OrdinalIgnoreCase))
                .ToList();

            var standingsDict = new Dictionary<long, StandingDto>();

            foreach (var match in finishedMatches)
            {
                if (match.MatchTeams == null || match.MatchTeams.Count != 2) continue;

                var teamA = match.MatchTeams.ElementAt(0);
                var teamB = match.MatchTeams.ElementAt(1);

                if (!standingsDict.ContainsKey(teamA.TeamId))
                    standingsDict[teamA.TeamId] = new StandingDto
                    {
                        TeamId = teamA.TeamId,
                        TeamName = teamA.Team?.Name ?? "Equipo A",
                        TeamLogo = teamA.Team?.Shield != null ? Convert.ToBase64String(teamA.Team.Shield) : null,
                        CategoryId = teamA.Team?.CategoryId
                    };

                if (!standingsDict.ContainsKey(teamB.TeamId))
                    standingsDict[teamB.TeamId] = new StandingDto
                    {
                        TeamId = teamB.TeamId,
                        TeamName = teamB.Team?.Name ?? "Equipo B",
                        TeamLogo = teamB.Team?.Shield != null ? Convert.ToBase64String(teamB.Team.Shield) : null,
                        CategoryId = teamB.Team?.CategoryId
                    };

                var standingA = standingsDict[teamA.TeamId];
                var standingB = standingsDict[teamB.TeamId];

                standingA.MatchesPlayed++;
                standingB.MatchesPlayed++;

                standingA.GoalsFor += teamA.Goals;
                standingA.GoalsAgainst += teamB.Goals;
                standingB.GoalsFor += teamB.Goals;
                standingB.GoalsAgainst += teamA.Goals;

                standingA.GoalDifference = standingA.GoalsFor - standingA.GoalsAgainst;
                standingB.GoalDifference = standingB.GoalsFor - standingB.GoalsAgainst;

                if (teamA.Goals > teamB.Goals)
                {
                    standingA.Wins++;
                    standingA.Points += 3;
                    standingB.Losses++;
                }
                else if (teamA.Goals < teamB.Goals)
                {
                    standingB.Wins++;
                    standingB.Points += 3;
                    standingA.Losses++;
                }
                else
                {
                    standingA.Draws++;
                    standingA.Points += 1;
                    standingB.Draws++;
                    standingB.Points += 1;
                }
            }

            var standings = standingsDict.Values
                .OrderByDescending(s => s.Points)
                .ThenByDescending(s => s.GoalDifference)
                .ThenByDescending(s => s.GoalsFor)
                .ToList();

            return Ok(standings);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStanding([FromBody] Standing standing)
        {
            _context.standings.Add(standing);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStanding), new { id = standing.Id }, standing);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStanding(long id, [FromBody] Standing standing)
        {
            if (id != standing.Id) return BadRequest();
            _context.Entry(standing).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStanding(long id)
        {
            var standing = await _context.standings.FindAsync(id);
            if (standing == null) return NotFound();
            _context.standings.Remove(standing);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
