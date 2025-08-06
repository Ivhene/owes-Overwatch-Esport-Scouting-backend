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
            List<PlayerDTO> players = await _playerService.GetAllPlayers();

            return Ok(players);
        }

        [HttpGet("/players/{playerID}")]
        public async Task<IActionResult> GetPlayerByID(int playerID)
        {
            PlayerDTO player = await _playerService.GetPlayerByID(playerID);
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

        [HttpPut("/players/{playerID}")]
        public async Task<IActionResult> UpdatePlayer(int playerID, [FromBody] Player player)
        {
            if (player == null)
            {
                return BadRequest("Valid player data is required.");
            }

            PlayerDTO updatedPlayer = await _playerService.UpdatePlayer(player, playerID);
            if (updatedPlayer == null)
            {
                return NotFound();
            }
            return Ok(updatedPlayer);
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
