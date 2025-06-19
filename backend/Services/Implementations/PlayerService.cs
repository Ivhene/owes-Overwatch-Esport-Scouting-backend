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

        public Player AddPlayer(Player player)
        {
            return _playerRepository.AddPlayer(player);
        }

        public bool DeletePlayer(int playerID)
        {
            return _playerRepository.DeletePlayer(playerID);
        }

        public List<Player> GetAllPlayers()
        {
            return _playerRepository.GetAllPlayers();
        }

        public Player GetPlayerByGamertag(string gamertag)
        {
            return _playerRepository.GetPlayerByGamertag(gamertag);
        }

        public Player GetPlayerByID(int playerID)
        {
            return _playerRepository.GetPlayerByID(playerID);
        }

        public List<Player> GetPlayersByRegion(Regions region)
        {
            return _playerRepository.GetPlayersByRegion(region);
        }

        public List<Player> GetPlayersByTeam(int teamID)
        {
            return _playerRepository.GetPlayersByTeam(teamID);
        }

        public Player UpdatePlayer(Player player)
        {
            return _playerRepository.UpdatePlayer(player);
        }
    }
}
