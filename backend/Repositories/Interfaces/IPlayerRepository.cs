using backend.DTOs;
using backend.Enums;
using backend.Models;
using System.Collections.Generic;

namespace backend.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task<List<Player>> GetAllPlayers();

        Task<Player> GetPlayerByID(int playerID);

        Task<Player> GetPlayerByGamertag(string gamertag);

        Task<Player> AddPlayer(Player player);

        Task<Player> UpdatePlayer(Player player, int playerId);

        Task<bool> DeletePlayer(int playerID);

        Task<List<Player>> GetPlayersByTeam(int teamID);

        Task<List<Player>> GetPlayersByRegion(Regions region);
    }
}
