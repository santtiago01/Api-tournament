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
    public class MatchHistoryController : ControllerBase
    {
        private readonly AdminDbContext _context;
        private readonly IHubContext<PublicHub> _hub;
        public MatchHistoryController(AdminDbContext context, IHubContext<PublicHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        [HttpGet]
        public async Task<IActionResult> GetMatchHistory() =>
            Ok(await _context.matchhistories
                             .Include(mh => mh.Match)
                             .Include(mh => mh.Player)
                             .Include(mh => mh.Team)
                             .ToListAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchEvent(long id)
        {
            var matchEvent = await _context.matchhistories
                                           .Include(mh => mh.Match)
                                           .Include(mh => mh.Player)
                                           .Include(mh => mh.Team)
                                           .FirstOrDefaultAsync(mh => mh.Id == id);
            if (matchEvent == null) return NotFound();
            return Ok(matchEvent);
        }

        [HttpGet("bymatch/{matchId}")]
        public async Task<IActionResult> GetMatchHistoryByMatch(long matchId)
        {
            var events = await _context.matchhistories
                                 .Include(mh => mh.Match)
                                 .Include(mh => mh.Player)
                                 .Include(mh => mh.Team)
                                 .Where(mh => mh.MatchId == matchId)
                                 .ToListAsync();

            var dtos = events.Select(e => new MatchHistoryDto
            {
                Minute = e.Minute,
                EventType = e.EventType,
                PlayerName = e.Player != null 
                             ? e.Player.FirstName + (string.IsNullOrEmpty(e.Player.LastName) ? "" : " " + e.Player.LastName) 
                             : "",
                Team = e.Team == null ? null : new TeamDto { Id = e.Team.Id, Name = e.Team.Name }
            });

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMatchEvent([FromBody] MatchHistory matchEvent)
        {
            _context.matchhistories.Add(matchEvent);
            await _context.SaveChangesAsync();

            var dto = new MatchHistoryDto
            {
                Minute = matchEvent.Minute,
                EventType = matchEvent.EventType,
                PlayerName = matchEvent.Player != null 
                             ? matchEvent.Player.FirstName + (string.IsNullOrEmpty(matchEvent.Player.LastName) ? "" : " " + matchEvent.Player.LastName) 
                             : "",
                Team = matchEvent.Team == null ? null : new TeamDto { Id = matchEvent.Team.Id, Name = matchEvent.Team.Name }
            };

            await _hub.Clients.All.SendAsync("MatchHistoryUpdated", dto);
            return CreatedAtAction(nameof(GetMatchEvent), new { id = matchEvent.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatchEvent(long id, [FromBody] MatchHistory matchEvent)
        {
            if (id != matchEvent.Id) return BadRequest();
            _context.Entry(matchEvent).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var dto = new MatchHistoryDto
            {
                Minute = matchEvent.Minute,
                EventType = matchEvent.EventType,
                PlayerName = matchEvent.Player != null 
                            ? matchEvent.Player.FirstName + (string.IsNullOrEmpty(matchEvent.Player.LastName) ? "" : " " + matchEvent.Player.LastName) 
                            : "",
                Team = matchEvent.Team == null ? null : new TeamDto { Id = matchEvent.Team.Id, Name = matchEvent.Team.Name }
            };

            await _hub.Clients.All.SendAsync("MatchHistoryUpdated", dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatchEvent(long id)
        {
            var matchEvent = await _context.matchhistories.FindAsync(id);
            if (matchEvent == null) return NotFound();
            _context.matchhistories.Remove(matchEvent);
            await _context.SaveChangesAsync();

            var dto = new MatchHistoryDto
            {
                Minute = matchEvent.Minute,
                EventType = matchEvent.EventType,
                PlayerName = matchEvent.Player != null 
                            ? matchEvent.Player.FirstName + (string.IsNullOrEmpty(matchEvent.Player.LastName) ? "" : " " + matchEvent.Player.LastName) 
                            : "",
                Team = matchEvent.Team == null ? null : new TeamDto { Id = matchEvent.Team.Id, Name = matchEvent.Team.Name }
            };

            await _hub.Clients.All.SendAsync("MatchHistoryUpdated", dto);
            return NoContent();
        }
    }
}
