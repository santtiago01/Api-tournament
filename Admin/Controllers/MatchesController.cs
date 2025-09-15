using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;
using TournamentApi.Public.DTOs;
using TournamentApi.Public.Hubs;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly AdminDbContext _context;
        private readonly IHubContext<PublicHub> _hub;
        public MatchesController(AdminDbContext context, IHubContext<PublicHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        private bool MatchExists(long id)
        {
            return _context.matches.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> GetMatches()
        {
            var matches = await _context.matches
                .Include(m => m.Zone)
                .Include(m => m.Status)
                .Include(m => m.Tournament!)
                .Include(m => m.MatchTeams!).ThenInclude(mt => mt.Team)
                .ToListAsync();

            var dtos = matches.Select(match => new MatchDto
            {
                Id = match.Id,
                MatchDate = match.MatchDate,
                Status = match.Status == null ? null : new MatchStatusDto
                {
                    Id = match.Status.Id,
                    Name = match.Status.Name
                },
                Tournament = match.Tournament == null ? null : new TournamentDto
                {
                    Id = match.Tournament.Id,
                    Name = match.Tournament.Description,
                    StartDate = match.Tournament.StartDate,
                    EndDate = match.Tournament.FinishDate
                },
                Zone = match.Zone == null ? null : new ZoneDto
                {
                    Id = match.Zone.Id,
                    Name = match.Zone.Name
                },
                Teams = match.MatchTeams?.Select(mt => new MatchTeamDto
                {
                    Id = mt.Id,
                    TeamId = mt.TeamId,
                    Score = mt.Goals,
                    Team = mt.Team == null ? null : new TeamDto
                    {
                        Id = mt.Team.Id,
                        Name = mt.Team.Name,
                        Shield = mt.Team.Shield
                    }
                })
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchesById(long id)
        {
            var match = await _context.matches
                .Include(m => m.Zone)
                .Include(m => m.Status)
                .Include(m => m.Tournament!)
                .Include(m => m.MatchTeams!).ThenInclude(mt => mt.Team)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (match == null) return NotFound();

            var dto = new MatchDto
            {
                Id = match.Id,
                MatchDate = match.MatchDate,
                Status = match.Status == null ? null : new MatchStatusDto
                {
                    Id = match.Status.Id,
                    Name = match.Status.Name
                },
                Tournament = match.Tournament == null ? null : new TournamentDto
                {
                    Id = match.Tournament.Id,
                    Name = match.Tournament.Description,
                    StartDate = match.Tournament.StartDate,
                    EndDate = match.Tournament.FinishDate
                },
                Zone = match.Zone == null ? null : new ZoneDto
                {
                    Id = match.Zone.Id,
                    Name = match.Zone.Name
                },
                Teams = match.MatchTeams?.Select(mt => new MatchTeamDto
                {
                    Id = mt.Id,
                    TeamId = mt.TeamId,
                    Score = mt.Goals,
                    Team = mt.Team == null ? null : new TeamDto
                    {
                        Id = mt.Team.Id,
                        Name = mt.Team.Name,
                        Shield = mt.Team.Shield
                    }
                })
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatch([FromBody] Match match)
        {
            _context.matches.Add(match);
            await _context.SaveChangesAsync();

            // Recargar match con includes
            var createdMatch = await _context.matches
                .Include(m => m.Tournament!)
                .Include(m => m.MatchTeams!).ThenInclude(mt => mt.Team)
                .Include(m => m.Zone)
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == match.Id);

            if (createdMatch != null)
            {
                var dto = new MatchDto
                {
                    Id = createdMatch.Id,
                    MatchDate = createdMatch.MatchDate,
                    Status = createdMatch.Status == null ? null : new MatchStatusDto
                    {
                        Id = createdMatch.Status.Id,
                        Name = createdMatch.Status.Name
                    },
                    Tournament = createdMatch.Tournament == null ? null : new TournamentDto
                    {
                        Id = createdMatch.Tournament.Id,
                        Name = createdMatch.Tournament.Description,
                        StartDate = createdMatch.Tournament.StartDate,
                        EndDate = createdMatch.Tournament.FinishDate
                    },
                    Zone = createdMatch.Zone == null ? null : new ZoneDto
                    {
                        Id = createdMatch.Zone.Id,
                        Name = createdMatch.Zone.Name
                    },
                    Teams = createdMatch.MatchTeams?.Select(mt => new MatchTeamDto
                    {
                        Id = mt.Id,
                        TeamId = mt.TeamId,
                        Score = mt.Goals,
                        Team = mt.Team == null ? null : new TeamDto
                        {
                            Id = mt.Team.Id,
                            Name = mt.Team.Name,
                            Shield = mt.Team.Shield
                        }
                    })
                };

                await _hub.Clients.All.SendAsync("MatchUpdated", dto);
            }

            return CreatedAtAction(nameof(GetMatchesById), new { id = match.Id }, match);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(long id, [FromBody] Match match)
        {
            if (id != match.Id) return BadRequest();

            _context.Entry(match).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            // recargar con includes
            var updatedMatch = await _context.matches
                .Include(m => m.Tournament!)
                .Include(m => m.MatchTeams!).ThenInclude(mt => mt.Team)
                .Include(m => m.Zone)
                .Include(m => m.Status)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (updatedMatch == null) return NotFound();

            var dto = new MatchDto
            {
                Id = updatedMatch.Id,
                MatchDate = updatedMatch.MatchDate,
                Status = updatedMatch.Status == null ? null : new MatchStatusDto
                {
                    Id = updatedMatch.Status.Id,
                    Name = updatedMatch.Status.Name
                },
                Tournament = updatedMatch.Tournament == null ? null : new TournamentDto
                {
                    Id = updatedMatch.Tournament.Id,
                    Name = updatedMatch.Tournament.Description,
                    StartDate = updatedMatch.Tournament.StartDate,
                    EndDate = updatedMatch.Tournament.FinishDate
                },
                Zone = updatedMatch.Zone == null ? null : new ZoneDto
                {
                    Id = updatedMatch.Zone.Id,
                    Name = updatedMatch.Zone.Name
                },
                Teams = updatedMatch.MatchTeams?.Select(mt => new MatchTeamDto
                {
                    Id = mt.Id,
                    TeamId = mt.TeamId,
                    Score = mt.Goals,
                    Team = mt.Team == null ? null : new TeamDto
                    {
                        Id = mt.Team.Id,
                        Name = mt.Team.Name,
                        Shield = mt.Team.Shield
                    }
                })
            };

            await _hub.Clients.All.SendAsync("MatchUpdated", dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(long id)
        {
            var match = await _context.matches.FindAsync(id);
            if (match == null) return NotFound();

            _context.matches.Remove(match);
            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("MatchDeleted", id);
            return NoContent();
        }
    }
}