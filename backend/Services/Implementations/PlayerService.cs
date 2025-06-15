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
            throw new NotImplementedException();
        }

        public bool DeletePlayer(int playerID)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetAllPlayers()
        {
            throw new NotImplementedException();
        }

        public Player GetPlayerByGamertag(string gamertag)
        {
            throw new NotImplementedException();
        }

        public Player GetPlayerByID(int playerID)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetPlayersByRegion(Regions region)
        {
            throw new NotImplementedException();
        }

        public List<Player> GetPlayersByTeam(int teamID)
        {
            throw new NotImplementedException();
        }

        public Player UpdatePlayer(Player player)
        {
            throw new NotImplementedException();
        }
    }
}
