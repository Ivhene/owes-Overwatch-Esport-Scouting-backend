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
        /// A list of all the players as <see cref="CompletePlayerDTO"/>s.
        /// </returns>
        Task<List<CompletePlayerDTO>> GetAllPlayers();

        /// <summary>
        /// Retrieves a single player by its unique identifier.
        /// </summary>
        /// <param name="playerID">The database ID of the player.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> whose result is the <see cref="PlayerDTO"/> matching <paramref name="playerID"/>,
        /// or <c>null</c> if none is found.
        /// </returns>
        Task<CompletePlayerDTO> GetPlayerByID(int playerID);

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
        Task<CompletePlayerDTO?> UpdatePlayer(
    UpdatePlayerRequest request,
    int playerId);

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

        /// <summary>
        /// Gives a player a rating on a single hero, overwriting any existing rating
        /// the player has on that hero.
        /// </summary>
        /// <param name="playerId">The ID of the player being rated.</param>
        /// <param name="request">The hero, rating and rating user.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> whose result is the updated <see cref="CompletePlayerDTO"/>,
        /// or <c>null</c> if the player or hero does not exist.
        /// </returns>
        Task<CompletePlayerDTO?> RatePlayer(int playerId, RatePlayerRequest request);

        /// <summary>
        /// Deletes a single rating belonging to a player.
        /// </summary>
        /// <param name="playerId">The ID of the player the rating belongs to.</param>
        /// <param name="ratingId">The ID of the rating to remove.</param>
        /// <returns>
        /// A <see cref="Task{TResult}"/> whose result is <c>true</c> if the rating was deleted;
        /// <c>false</c> if no rating with that ID exists for the player.
        /// </returns>
        Task<bool> DeleteRating(int playerId, long ratingId);
    }
}
