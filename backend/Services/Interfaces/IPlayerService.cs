using backend.Enums;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface IPlayerService
    {
        public List<Player> GetAllPlayers();

        public Player GetPlayerByID(int playerID);

        public Player GetPlayerByGamertag(string gamertag);

        public Player AddPlayer(Player player);

        public Player UpdatePlayer(Player player);

        public bool DeletePlayer(int playerID);

        public List<Player> GetPlayersByTeam(int teamID);

        public List<Player> GetPlayersByRegion(Regions region);
    }
}
