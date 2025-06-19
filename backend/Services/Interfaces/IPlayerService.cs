using backend.DTOs;
using backend.Enums;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IPlayerService
    {
        public Task<List<PlayerDTO>> GetAllPlayers();

        public Task<PlayerDTO> GetPlayerByID(int playerID);

        public Task<PlayerDTO> GetPlayerByGamertag(string gamertag);

        public Task<PlayerDTO> AddPlayer(Player player);

        public Task<PlayerDTO> UpdatePlayer(Player player, int playerId);

        public Task<bool> DeletePlayer(int playerID);

        public Task<List<PlayerDTO>> GetPlayersByTeam(int teamID);

        public Task<List<PlayerDTO>> GetPlayersByRegion(Regions region);
    }
}
