using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using TournamentApi.Admin.Models;
using TournamentApi.Admin.Dtos;
using TournamentApi.Data;
using TournamentApi.Public.Hubs;

namespace TournamentApi.Admin.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class BracketMatchResultController : ControllerBase
    {
        private readonly AdminDbContext _context;
        private readonly IHubContext<PublicHub> _hub;

        public BracketMatchResultController(AdminDbContext context, IHubContext<PublicHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        [HttpGet("{bracketMatchId}")]
        public async Task<IActionResult> GetResults(int bracketMatchId)
        {
            var results = await _context.bracketmatchresults
                .Where(r => r.BracketMatchId == bracketMatchId)
                .OrderByDescending(r => r.RecordedAt)
                .ToListAsync();
            return Ok(results);
        }

        [HttpPost]
        public async Task<IActionResult> AddResult([FromBody] BracketMatchResult result)
        {
            _context.bracketmatchresults.Add(result);
            await _context.SaveChangesAsync();

            // construir DTO desde el modelo
            var dto = new BracketMatchResultDto
            {
                Id = result.Id,
                MatchId = result.BracketMatchId,
                WinnerTeamId = result.WinnerTeamId,
                LoserTeamId = (result.HomeGoals > result.AwayGoals)
                                ? result.BracketMatch?.AwayTeamId
                                : result.HomeGoals < result.AwayGoals
                                    ? result.BracketMatch?.HomeTeamId
                                    : null,
                WinnerScore = Math.Max(result.HomeGoals, result.AwayGoals),
                LoserScore = Math.Min(result.HomeGoals, result.AwayGoals)
            };

            await _hub.Clients.All.SendAsync("BracketMatchResultAdded", dto);

            return Ok(dto);
        }
    }
}
