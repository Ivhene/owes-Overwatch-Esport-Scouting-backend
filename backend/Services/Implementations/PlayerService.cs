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

        public async Task<CompletePlayerDTO?> UpdatePlayer(
    UpdatePlayerRequest request,
    int playerId)
        {
            Player player = new Player
            {
                Gamertag = request.Gamertag,
                RealName = request.RealName,
                Birthday = request.Birthday,
                PlayerRole = request.PlayerRole,
                NativeRegion = request.NativeRegion,
                PlayerImage = request.PlayerImage,
                CurrentTeam = request.CurrentTeam
            };

            Player? updatedPlayer =
                await _playerRepository.UpdatePlayer(player, playerId);

            if (updatedPlayer == null)
            {
                return null;
            }

            await _playerRepository.UpdateRatings(
                playerId,
                request.Ratings);

            return await _playerRepository.GetPlayerByID(playerId);
        }

        public async Task<CompletePlayerDTO?> RatePlayer(int playerId, RatePlayerRequest request)
        {
            Rating? rating = await _playerRepository.UpsertRating(
                playerId,
                request.HeroId,
                request.Rating!.Value,
                request.RatingUser);

            if (rating == null)
            {
                return null;
            }

            return await _playerRepository.GetPlayerByID(playerId);
        }

        public Task<bool> DeleteRating(int playerId, long ratingId)
        {
            return _playerRepository.DeleteRating(playerId, ratingId);
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
