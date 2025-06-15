using System.Collections.Generic;
using backend.Enums;
using backend.Models;

namespace backend.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        List<Player> GetAllPlayers();

        Player GetPlayerByID(int playerID);

        Player GetPlayerByGamertag(string gamertag);

        Player AddPlayer(Player player);

        Player UpdatePlayer(Player player);

        bool DeletePlayer(int playerID);

        List<Player> GetPlayersByTeam(int teamID);

        List<Player> GetPlayersByRegion(Regions region);
    }
}
