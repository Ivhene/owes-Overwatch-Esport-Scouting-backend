using backend.DTOs;
using backend.Enums;
using backend.Models;
using backend.Repositories.Interfaces;
using Newtonsoft.Json;
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

        public async Task<List<CompletePlayerDTO>> GetAllPlayers()
        {
            const string rawColumns = @"
            player_id,
            gamertag,
            real_name,
            birthday,
            native_region,
            player_image,
            role:role (
              role_id,
              role_name,
              role_image
            ),
            current_team:team (
              team_id,
              team_name,
              team_image,
              competing_region
            ),
            ratings:rating (
              rating_id,
              rating,
              rating_user,
              hero:hero (
                hero_id,
                hero_name,
                hero_image,
                hero_role
              )
            )
        ";

            // 1) do the PostgREST join-select on your Player table
            var resp = await _supabase
                .From<Player>()               // must be your BaseModel type
                .Select(rawColumns)
                .Get();

            // 2) pull out the raw JSON string
            //    (ModeledResponse<T> inherits BaseResponse, which has a Content property)
            string json = resp.Content;      // :contentReference[oaicite:0]{index=0}

            // 3) deserialize into your RawPlayerRow types
            var rawRows = JsonConvert
                .DeserializeObject<List<RawPlayerRow>>(json)
                ?? new List<RawPlayerRow>();

            // 4) map into your immutable DTOs
            return rawRows
                .Select(r => new CompletePlayerDTO
                {
                    PlayerId = r.PlayerId,
                    Gamertag = r.Gamertag,
                    RealName = r.RealName,
                    Birthday = r.Birthday,
                    NativeRegion = r.NativeRegion,
                    PlayerImage = r.PlayerImage,

                    Role = new RoleDTO
                    {
                        RoleId = r.Role.RoleId,
                        RoleName = r.Role.RoleName,
                        RoleImage = r.Role.RoleImage
                    },

                    CurrentTeam = r.CurrentTeam is null
                        ? null
                        : new TeamDTO
                        {
                            TeamId = r.CurrentTeam.TeamId,
                            TeamName = r.CurrentTeam.TeamName,
                            TeamImage = r.CurrentTeam.TeamImage,
                            CompetingRegion = r.CurrentTeam.CompetingRegion
                        },

                    Ratings = r.Ratings
                        .Select(rt => new RatingDTO
                        {
                            RatingId = rt.RatingId,
                            Ratings = rt.Ratings,
                            RatingUser = rt.RatingUser,
                            Hero = new HeroDTO
                            {
                                HeroId = rt.Hero.HeroId,
                                HeroName = rt.Hero.HeroName,
                                HeroImage = rt.Hero.HeroImage,
                                HeroRole = rt.Hero.HeroRole
                            }
                        })
                        .ToList()
                })
                .ToList();
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
