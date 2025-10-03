using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class GroupStagesController : ControllerBase
    {
        private readonly AdminDbContext _context;

        public GroupStagesController(AdminDbContext context)
        {
            _context = context;
        }

        // GET: api/GroupStages/tournament/1
        [HttpGet("tournament/{tournamentId}")]
        public async Task<IActionResult> GetByTournament(int tournamentId)
        {
            try
            {
                var groups = await _context.groupstages
                    .Where(g => g.TournamentId == tournamentId)
                    .Select(g => new
                    {
                        g.Id,
                        g.Name,
                        Teams = g.Teams
                            .Select(gt => new 
                            {
                                gt.Id,
                                gt.TeamId,
                                TeamName = gt.Team != null ? gt.Team.Name : null,
                                gt.Points,
                                gt.Played,
                                gt.Wins,
                                gt.Draws,
                                gt.Losses,
                                gt.GoalsFor,
                                gt.GoalsAgainst
                            }).ToList()
                    })
                    .ToListAsync();

                return Ok(groups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        // POST: api/GroupStages
        [HttpPost]
        public async Task<IActionResult> Create(GroupStage groupStage)
        {
            _context.groupstages.Add(groupStage);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByTournament), new { tournamentId = groupStage.TournamentId }, groupStage);
        }

        // PUT: api/GroupStages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GroupStage updated)
        {
            var group = await _context.groupstages.FindAsync(id);
            if (group == null) return NotFound();

            group.Name = updated.Name;
            group.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/GroupStages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var group = await _context.groupstages.FindAsync(id);
            if (group == null) return NotFound();

            _context.groupstages.Remove(group);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
