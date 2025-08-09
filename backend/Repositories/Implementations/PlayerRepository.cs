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

            List<RawPlayerRow> data = (await _supabase
                .From<RawPlayerRow>()
                .Select(rawColumns)
                .Get())
                .Models ?? new List<RawPlayerRow>(); 


            return data.Select(r => new CompletePlayerDTO
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

        public async Task<CompletePlayerDTO> GetPlayerByID(int playerID)
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

            RawPlayerRow data = (await _supabase
                .From<RawPlayerRow>()
                .Where(player => player.PlayerId == playerID)
                .Select(rawColumns)
                .Get())
                .Models
                .FirstOrDefault() ?? new RawPlayerRow();

            return data != null ? new CompletePlayerDTO
            {
                PlayerId = data.PlayerId,
                Gamertag = data.Gamertag,
                RealName = data.RealName,
                Birthday = data.Birthday,
                NativeRegion = data.NativeRegion,
                PlayerImage = data.PlayerImage,
                Role = new RoleDTO
                {
                    RoleId = data.Role.RoleId,
                    RoleName = data.Role.RoleName,
                    RoleImage = data.Role.RoleImage
                },
                CurrentTeam = data.CurrentTeam is null
                    ? null
                    : new TeamDTO
                    {
                        TeamId = data.CurrentTeam.TeamId,
                        TeamName = data.CurrentTeam.TeamName,
                        TeamImage = data.CurrentTeam.TeamImage,
                        CompetingRegion = data.CurrentTeam.CompetingRegion
                    },
                Ratings = data.Ratings
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
            } : new CompletePlayerDTO();
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
