using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Admin.Models.DTOs;
using TournamentApi.Data;
using TournamentApi.Public.Hubs;

namespace TournamentApi.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class GroupStageMatchesController : ControllerBase
    {
        private readonly AdminDbContext _context;
        private readonly IHubContext<PublicHub> _hub;

        public GroupStageMatchesController(AdminDbContext context, IHubContext<PublicHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var matches = await _context.groupstagematches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.GroupStage)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return Ok(matches);
        }

        // GET: api/admin/GroupStageMatches/group/1
        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetByGroup(int groupId)
        {
            var matches = await _context.groupstagematches
                .Where(m => m.GroupStageId == groupId)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

            return Ok(matches);
        }

        // POST: api/admin/GroupStageMatches
        [HttpPost]
        public async Task<IActionResult> CreateGroupStageMatch([FromBody] GroupStageMatch match)
        {
            _context.groupstagematches.Add(match);
            await _context.SaveChangesAsync();

            var createdMatch = await _context.groupstagematches
                .Include(m => m.GroupStage)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .FirstOrDefaultAsync(m => m.Id == match.Id);

            if (createdMatch != null)
            {
                var dto = new GroupStageMatchDto
                {
                    Id = createdMatch.Id,
                    GroupStageId = createdMatch.GroupStageId,
                    HomeTeam = createdMatch.HomeTeam?.Name,
                    AwayTeam = createdMatch.AwayTeam?.Name,
                    HomeGoals = createdMatch.HomeGoals,
                    AwayGoals = createdMatch.AwayGoals
                };

                await _hub.Clients.All.SendAsync("GroupStageMatchCreated", dto);
            }

            return Ok(createdMatch);
        }

        // PUT: api/admin/GroupStageMatches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroupStageMatch(int id, [FromBody] GroupStageMatch groupstagematch)
        {
            var match = await _context.groupstagematches.FindAsync(id);
            if (match == null) return NotFound();

            match.HomeGoals = groupstagematch.HomeGoals;
            match.AwayGoals = groupstagematch.AwayGoals;

            await _context.SaveChangesAsync();

            await _hub.Clients.All.SendAsync("GroupStageMatchUpdate", new
            {
                match.Id,
                match.GroupStageId,
                match.HomeGoals,
                match.AwayGoals
            });

            return NoContent();
        }

        // DELETE: api/admin/GroupStageMatches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var match = await _context.groupstagematches.FindAsync(id);
            if (match == null) return NotFound();

            _context.groupstagematches.Remove(match);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
