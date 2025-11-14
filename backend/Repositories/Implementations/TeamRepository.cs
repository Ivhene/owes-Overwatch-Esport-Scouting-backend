using backend.DTOs;
using backend.Models;
using Supabase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories.Implementations
{
    public class TeamRepository
    {
        private readonly Client _supabase;

        public TeamRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<List<RawTeamRow>> GetAllTeams()
        {
            const string columns = @"
                team_id,
                team_name,
                team_image,
                competing_region,
                players:player (
                  player_id,
                  gamertag,
                  real_name,
                  birthday,
                  native_region,
                  player_image,
                  player_role,
                  role:role (
                    role_id,
                    role_name,
                    role_image
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
                )
            ";

            return (await _supabase
                .From<RawTeamRow>()
                .Select(columns)
                .Get())
                .Models ?? new List<RawTeamRow>();
        }

        public async Task<RawTeamRow?> GetTeamByID(int teamID)
        {
            const string columns = @"
                team_id,
                team_name,
                team_image,
                competing_region,
                players:player (
                  player_id,
                  gamertag,
                  real_name,
                  birthday,
                  native_region,
                  player_image,
                  player_role,
                  role:role (
                    role_id,
                    role_name,
                    role_image
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
                )
            ";

            return (await _supabase
                .From<RawTeamRow>()
                .Where(t => t.TeamId == teamID)
                .Select(columns)
                .Get())
                .Models
                .FirstOrDefault();
        }

        public async Task<Team> AddTeam(Team team)
        {
            return (await _supabase.From<Team>().Insert(team)).Model ?? new Team();
        }

        public async Task<Team> UpdateTeam(Team team, int teamID)
        {
            return (await _supabase.From<Team>()
                .Where(t => t.TeamId == teamID)
                .Set(t => t.TeamName, team.TeamName)
                .Set(t => t.TeamImage, team.TeamImage)
                .Set(t => t.CompetingRegion, team.CompetingRegion)
                .Update())
                .Model ?? new Team();
        }

        public async Task<bool> DeleteTeam(int teamID)
        {
            await _supabase.From<Team>().Where(t => t.TeamId == teamID).Delete();
            return true;
        }
    }
}
