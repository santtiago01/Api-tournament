using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentApi.Admin.Models;
using TournamentApi.Data;

namespace TournamentApi.Admin.Controllers
{
    [ApiController]
    [Route("api/admin/[controller]")]
    public class GroupStageQualifiersController : ControllerBase
    {
        private readonly AdminDbContext _context;

        public GroupStageQualifiersController(AdminDbContext context)
        {
            _context = context;
        }

        // GET: api/admin/GroupStageQualifiers/group/1
        [HttpGet("group/{groupId}")]
        public async Task<IActionResult> GetByGroup(int groupId)
        {
            var qualifiers = await _context.groupstagequalifiers
                .Where(q => q.GroupStageId == groupId)
                .Include(q => q.Stage)
                .Include(q => q.BracketMatch)
                .ToListAsync();

            return Ok(qualifiers);
        }

        // POST: api/admin/GroupStageQualifiers
        [HttpPost]
        public async Task<IActionResult> Create(GroupStageQualifier qualifier)
        {
            _context.groupstagequalifiers.Add(qualifier);
            await _context.SaveChangesAsync();
            return Ok(qualifier);
        }

        // PUT: api/admin/GroupStageQualifiers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, GroupStageQualifier updated)
        {
            var qualifier = await _context.groupstagequalifiers.FindAsync(id);
            if (qualifier == null) return NotFound();

            qualifier.StageId = updated.StageId;
            qualifier.Position = updated.Position;
            qualifier.BracketMatchId = updated.BracketMatchId;
            qualifier.Slot = updated.Slot;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/admin/GroupStageQualifiers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var qualifier = await _context.groupstagequalifiers.FindAsync(id);
            if (qualifier == null) return NotFound();

            _context.groupstagequalifiers.Remove(qualifier);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
