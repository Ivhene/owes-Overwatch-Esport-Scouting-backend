using backend.DTOs;
using backend.Models;
using backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet("/players")]
        public async Task<IActionResult> GetAllPlayers()
        {
            List<CompletePlayerDTO> players = await _playerService.GetAllPlayers();

            return Ok(players);
        }

        [HttpGet("/players/{playerID}")]
        public async Task<IActionResult> GetPlayerByID(int playerID)
        {
            CompletePlayerDTO player = await _playerService.GetPlayerByID(playerID);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        [HttpPost("/players")]
        public async Task<IActionResult> AddPlayer([FromBody] Player player)
        {
            if (player == null)
            {
                return BadRequest("Valid player data is required.");
            }
            PlayerDTO createdPlayer = await _playerService.AddPlayer(player);
            return CreatedAtAction(nameof(GetPlayerByID), new { playerID = createdPlayer.PlayerId }, createdPlayer);
        }

        [HttpPut("/players/{playerID:int}")]
        public async Task<IActionResult> UpdatePlayer(
    int playerID,
    [FromBody] UpdatePlayerRequest request)
        {
            CompletePlayerDTO? updatedPlayer =
                await _playerService.UpdatePlayer(request, playerID);

            if (updatedPlayer == null)
            {
                return NotFound();
            }

            return Ok(updatedPlayer);
        }

        [HttpPost("/players/{playerID:int}/ratings")]
        public async Task<IActionResult> RatePlayer(
    int playerID,
    [FromBody] RatePlayerRequest request)
        {
            CompletePlayerDTO? ratedPlayer =
                await _playerService.RatePlayer(playerID, request);

            if (ratedPlayer == null)
            {
                return NotFound("Player or hero not found.");
            }

            return Ok(ratedPlayer);
        }

        [HttpDelete("/players/{playerID:int}/ratings/{ratingID:long}")]
        public async Task<IActionResult> DeleteRating(int playerID, long ratingID)
        {
            bool deleted = await _playerService.DeleteRating(playerID, ratingID);
            if (!deleted)
            {
                return NotFound("Rating not found for this player.");
            }
            return NoContent();
        }

        [HttpDelete("/players/{playerID}")]
        public async Task<IActionResult> DeletePlayer(int playerID)
        {
            bool deleted = await _playerService.DeletePlayer(playerID);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
