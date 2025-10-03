using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class GroupStageTeamsController : ControllerBase
    {
        private readonly AdminDbContext _context;

        public GroupStageTeamsController(AdminDbContext context)
        {
            _context = context;
        }

        // GET: api/GroupStageTeams/group/1
        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetByGroup(int groupId)
        {
            var teams = await _context.groupstageteams
                .Where(gt => gt.GroupStageId == groupId)
                .Include(gt => gt.Team)
                .ToListAsync();

            return Ok(teams);
        }

        // POST: api/GroupStageTeams
        [HttpPost]
        public async Task<IActionResult> AddTeam(GroupStageTeam groupTeam)
        {
            _context.groupstageteams.Add(groupTeam);
            await _context.SaveChangesAsync();
            return Ok(groupTeam);
        }

        // PUT: api/GroupStageTeams/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStats(int id, GroupStageTeam updated)
        {
            var groupTeam = await _context.groupstageteams.FindAsync(id);
            if (groupTeam == null) return NotFound();

            groupTeam.Points = updated.Points;
            groupTeam.Played = updated.Played;
            groupTeam.Wins = updated.Wins;
            groupTeam.Draws = updated.Draws;
            groupTeam.Losses = updated.Losses;
            groupTeam.GoalsFor = updated.GoalsFor;
            groupTeam.GoalsAgainst = updated.GoalsAgainst;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/GroupStageTeams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTeam(int id)
        {
            var groupTeam = await _context.groupstageteams.FindAsync(id);
            if (groupTeam == null) return NotFound();

            _context.groupstageteams.Remove(groupTeam);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
