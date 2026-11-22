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
        Task<CompletePlayerDTO> GetPlayerByID(int playerID);

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
        Task<Player?> UpdatePlayer(Player player, int playerId);

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

        /// <summary>
        /// Updates the ratings for a specific player.
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="ratings"></param>
        /// <returns></returns>
        Task UpdateRatings(int playerId, IEnumerable<UpdatePlayerRatingRequest> ratings);

        /// <summary>
        /// Gives a player a rating on a hero. If the player already has a rating
        /// on that hero it is overwritten, otherwise a new rating is created.
        /// </summary>
        /// <param name="playerId">The ID of the player being rated.</param>
        /// <param name="heroId">The ID of the hero the rating applies to.</param>
        /// <param name="rating">The <see cref="Ratings"/> value to give.</param>
        /// <param name="ratingUser">The user giving the rating.</param>
        /// <returns>
        /// The created or updated <see cref="Rating"/>, or <c>null</c> if the player or hero does not exist.
        /// </returns>
        Task<Rating?> UpsertRating(int playerId, int heroId, Ratings rating, string? ratingUser);

        /// <summary>
        /// Deletes a single rating belonging to a player.
        /// </summary>
        /// <param name="playerId">The ID of the player the rating belongs to.</param>
        /// <param name="ratingId">The ID of the rating to remove.</param>
        /// <returns>
        /// <c>true</c> if the rating was deleted; <c>false</c> if no rating with that ID exists for the player.
        /// </returns>
        Task<bool> DeleteRating(int playerId, long ratingId);
    }
}
