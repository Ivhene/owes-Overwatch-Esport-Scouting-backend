using backend.DTOs;
using backend.Models;
using backend.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Supabase.Postgrest.Exceptions;
using System.Text.Json;
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

            TeamDTO created;
            try
            {
                created = await _teamService.AddTeam(team);
            }
            catch (PostgrestException ex) when (TryGetValueTooLongMessage(ex, out string message))
            {
                return BadRequest(message);
            }
            return CreatedAtAction(nameof(GetTeamByID), new { teamID = created.TeamId }, created);
        }

        [HttpPut("/teams/{teamID}")]
        public async Task<IActionResult> UpdateTeam(int teamID, [FromBody] Team team)
        {
            if (team == null)
            {
                return BadRequest("Valid team data is required.");
            }

            TeamDTO? updated;
            try
            {
                updated = await _teamService.UpdateTeam(team, teamID);
            }
            catch (PostgrestException ex) when (TryGetValueTooLongMessage(ex, out string message))
            {
                return BadRequest(message);
            }
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

        // Postgres 22001 = string_data_right_truncation (value longer than the varchar column)
        private static bool TryGetValueTooLongMessage(PostgrestException ex, out string message)
        {
            message = string.Empty;
            if (string.IsNullOrEmpty(ex.Content))
            {
                return false;
            }

            try
            {
                using JsonDocument doc = JsonDocument.Parse(ex.Content);
                if (doc.RootElement.ValueKind != JsonValueKind.Object
                    || !doc.RootElement.TryGetProperty("code", out JsonElement code)
                    || code.GetString() != "22001")
                {
                    return false;
                }

                message = doc.RootElement.TryGetProperty("message", out JsonElement msg)
                    ? msg.GetString() ?? "Value too long."
                    : "Value too long.";
                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
