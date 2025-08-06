using backend.Enums;
using backend.Models;
using backend.Repositories.Interfaces;
using Supabase;

namespace backend.Repositories.Implementations
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly Client _supabase;

        public PlayerRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<Player> AddPlayer(Player player)
        {
            return (await _supabase.From<Player>().Insert(player)).Model ?? new Player();
        }

        public async Task<bool> DeletePlayer(int playerID)
        {
            await _supabase.From<Player>().Where(player => player.PlayerId == playerID).Delete();

            return true;
        }

        public async Task<List<Player>> GetAllPlayers()
        {
            var result = await _supabase.From<Player>().Get();
            return result.Models ?? new List<Player>();
        }

        public async Task<Player> GetPlayerByID(int playerID)
        {
            return (await _supabase.From<Player>().Where(player => player.PlayerId == playerID).Get()).Models?.FirstOrDefault() ?? new Player();
        }

        public async Task<List<Player>> GetPlayersByRegion(Regions region)
        {
            return (await _supabase.From<Player>().Where(player => player.NativeRegion == region).Get()).Models ?? new List<Player>();
        }

        public async Task<List<Player>> GetPlayersByTeam(int teamID)
        {
            return (await _supabase.From<Player>().Where(player => player.CurrentTeam == teamID).Get()).Models ?? new List<Player>();
        }

        public async Task<Player> UpdatePlayer(Player player, int playerId)
        {
            return (await _supabase.From<Player>()
                .Where(p => p.PlayerId == playerId)
                .Set(p => p.Gamertag, player.Gamertag)
                .Set(p => p.NativeRegion, player.NativeRegion)
                .Set(p => p.RealName, player.RealName)
                .Set(p => p.Birthday, player.Birthday)
                .Set(p => p.PlayerRole, player.PlayerRole)
                .Set(p => p.PlayerImage, player.PlayerImage)
                .Set(p => p.CurrentTeam, player.CurrentTeam)
                .Update())
                .Model ?? new Player();
        }
    }
}
