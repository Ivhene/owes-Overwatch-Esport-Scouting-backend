using backend.DTOs;
using backend.Enums;
using backend.Models;

namespace backend.Services.Interfaces
{
    /// <summary>
    /// A service interface for managing player data.
    /// </summary>
    public interface IPlayerService
    {
        /// <summary>
        /// Retrieves all players.
        /// </summary>
        /// <returns>
        /// A list of all the players as <see cref="PlayerDTO"/>s.
        /// </returns>
        Task<List<PlayerDTO>> GetAllPlayers();

        /// <summary>
        /// Retrieves a single player by its unique identifier.
        /// </summary>
        /// <param name="playerID">The database ID of the player.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> whose result is the <see cref="PlayerDTO"/> matching <paramref name="playerID"/>,
        /// or <c>null</c> if none is found.
        /// </returns>
        Task<PlayerDTO> GetPlayerByID(int playerID);

        /// <summary>
        /// Retrieves a single player by gamertag.
        /// </summary>
        /// <param name="gamertag">The gamertag string.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> whose result is the <see cref="PlayerDTO"/> matching <paramref name="gamertag"/>,
        /// or <c>null</c> if none is found.
        /// </returns>
        Task<PlayerDTO> GetPlayerByGamertag(string gamertag);

        /// <summary>
        /// Creates a new player.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> domain model to add.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> whose result is the created <see cref="PlayerDTO"/>.
        /// </returns>
        Task<PlayerDTO> AddPlayer(Player player);

        /// <summary>
        /// Updates an existing player.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> model containing updated data.</param>
        /// <param name="playerId">The ID of the player to update.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> whose result is the updated <see cref="PlayerDTO"/>.
        /// </returns>
        Task<PlayerDTO> UpdatePlayer(Player player, int playerId);

        /// <summary>
        /// Deletes a player by ID.
        /// </summary>
        /// <param name="playerID">The ID of the player to remove.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> whose result is <c>true</c> if deletion succeeded;
        /// otherwise <c>false</c>.
        /// </returns>
        Task<bool> DeletePlayer(int playerID);

        /// <summary>
        /// Retrieves all players from to a specific team.
        /// </summary>
        /// <param name="teamID">The ID of the team.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> whose result is the list of <see cref="PlayerDTO"/>s for that team.
        /// </returns>
        Task<List<PlayerDTO>> GetPlayersByTeam(int teamID);

        /// <summary>
        /// Retrieves all players from a given region.
        /// </summary>
        /// <param name="region">The <see cref="Regions"/> enum value.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> whose result is the list of <see cref="PlayerDTO"/>s in <paramref name="region"/>.
        /// </returns>
        Task<List<PlayerDTO>> GetPlayersByRegion(Regions region);
    }
}
