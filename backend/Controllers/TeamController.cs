using backend.DTOs;
using backend.Models;
using backend.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace backend.Controllers
{
    [ApiController]
    public class TeamController : ControllerBase
    {
         private readonly TeamService _teamService;

        public TeamController(TeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("/teams")]
        public async Task<IActionResult> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeams();
            return Ok(teams);
        }

        [HttpGet("/teams/{teamID}")]
        public async Task<IActionResult> GetTeamByID(int teamID)
        {
            CompleteTeamDTO? team = await _teamService.GetTeamByID(teamID);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        [HttpPost("/teams")]
        public async Task<IActionResult> AddTeam([FromBody] Team team)
        {
            if (team == null)
            {
                return BadRequest("Valid team data is required.");
            }

            TeamDTO created = await _teamService.AddTeam(team);
            return CreatedAtAction(nameof(GetTeamByID), new { teamID = created.TeamId }, created);
        }

        [HttpPut("/teams/{teamID}")]
        public async Task<IActionResult> UpdateTeam(int teamID, [FromBody] Team team)
        {
            if (team == null)
            {
                return BadRequest("Valid team data is required.");
            }

            TeamDTO? updated = await _teamService.UpdateTeam(team, teamID);
            if (updated == null)
            {
                return NotFound();
            }

            return Ok(updated);
        }

        [HttpDelete("/teams/{teamID}")]
        public async Task<IActionResult> DeleteTeam(int teamID)
        {
            bool deleted = await _teamService.DeleteTeam(teamID);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
