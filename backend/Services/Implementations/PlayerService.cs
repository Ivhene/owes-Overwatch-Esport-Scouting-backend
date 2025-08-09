using backend.DTOs;
using backend.Enums;
using backend.Models;
using backend.Repositories.Interfaces;
using backend.Services.Interfaces;

namespace backend.Services.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<PlayerDTO> AddPlayer(Player player)
        {
            return ConvertToDTO(await _playerRepository.AddPlayer(player));
        }

        public Task<bool> DeletePlayer(int playerID)
        {
            return _playerRepository.DeletePlayer(playerID);
        }

        public Task<List<CompletePlayerDTO>> GetAllPlayers()
        {
            return _playerRepository.GetAllPlayers();
        }

        public async Task<CompletePlayerDTO> GetPlayerByID(int playerID)
        {
            return await _playerRepository.GetPlayerByID(playerID);
        }

        public Task<List<PlayerDTO>> GetPlayersByRegion(Regions region)
        {
            return _playerRepository.GetPlayersByRegion(region)
                .ContinueWith(task => task.Result.Select(ConvertToDTO).ToList());
        }

        public Task<List<PlayerDTO>> GetPlayersByTeam(int teamID)
        {
            return _playerRepository.GetPlayersByTeam(teamID)
                .ContinueWith(task => task.Result.Select(ConvertToDTO).ToList());
        }

        public async Task<PlayerDTO> UpdatePlayer(Player player, int playerId)
        {
            return ConvertToDTO(await _playerRepository.UpdatePlayer(player, playerId));
        }

        private PlayerDTO ConvertToDTO(Player player)
        {
            return new PlayerDTO
            {
                PlayerId = player.PlayerId,
                Gamertag = player.Gamertag,
                CurrentTeam = player.CurrentTeam,
                RealName = player.RealName,
                Birthday = player.Birthday,
                PlayerRole = player.PlayerRole,
                NativeRegion = player.NativeRegion,
                PlayerImage = player.PlayerImage
            };
        }
    }
}
