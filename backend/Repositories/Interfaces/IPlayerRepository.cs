using backend.DTOs;
using backend.Enums;
using backend.Models;
using System.Collections.Generic;

namespace backend.Repositories.Interfaces
{
    /// <summary>
    /// A repository interface for managing player data persistence.
    /// </summary>
    public interface IPlayerRepository
    {
        /// <summary>
        /// Retrieves all players.
        /// </summary>
        /// <returns>
        /// A list of all the <see cref="Player"/> entities.
        /// </returns>
        Task<List<CompletePlayerDTO>> GetAllPlayers();

        /// <summary>
        /// Retrieves a single player by its unique identifier.
        /// </summary>
        /// <param name="playerID">The database ID of the player.</param>
        /// <returns>
        /// The <see cref="Player"/> matching <paramref name="playerID"/>,
        /// or <c>null</c> if none is found.
        /// </returns>
        Task<Player> GetPlayerByID(int playerID);

        /// <summary>
        /// Creates a new player record.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> domain model to add.</param>
        /// <returns>
        /// The created <see cref="Player"/>.
        /// </returns>
        Task<Player> AddPlayer(Player player);

        /// <summary>
        /// Updates an existing player.
        /// </summary>
        /// <param name="player">The <see cref="Player"/> model containing updated data.</param>
        /// <param name="playerId">The ID of the player to update.</param>
        /// <returns>
        /// The updated <see cref="Player"/>.
        /// </returns>
        Task<Player> UpdatePlayer(Player player, int playerId);

        /// <summary>
        /// Deletes a player by ID.
        /// </summary>
        /// <param name="playerID">The ID of the player to remove.</param>
        /// <returns>
        /// <c>true</c> if deletion succeeded; otherwise <c>false</c>.
        /// </returns>
        Task<bool> DeletePlayer(int playerID);

        /// <summary>
        /// Retrieves all players from a specific team.
        /// </summary>
        /// <param name="teamID">The ID of the team.</param>
        /// <returns>
        /// A list of the <see cref="Player"/> entities belonging to that team.
        /// </returns>
        Task<List<Player>> GetPlayersByTeam(int teamID);

        /// <summary>
        /// Retrieves all players from a given region.
        /// </summary>
        /// <param name="region">The <see cref="Regions"/> enum value.</param>
        /// <returns>
        /// A list of the <see cref="Player"/> entities in <paramref name="region"/>.
        /// </returns>
        Task<List<Player>> GetPlayersByRegion(Regions region);
    }
}
