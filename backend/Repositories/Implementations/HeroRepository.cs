using backend.DTOs;
using backend.Models;
using Supabase;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Repositories.Implementations
{
    public class HeroRepository
    {
        private readonly Client _supabase;

        public HeroRepository(Client supabase)
        {
            _supabase = supabase;
        }

        public async Task<List<RawHeroRow>> GetAllHeroes()
        {
            const string columns = @"
                hero_id,
                hero_name,
                hero_image,
                hero_role,
                role:role (
                  role_id,
                  role_name,
                  role_image
                ),
                ratings:rating (
                  rating_id,
                  rating,
                  rating_user,
                  player:player (
                    player_id,
                    gamertag,
                    real_name,
                    birthday,
                    native_region,
                    player_image,
                    player_role,
                    current_team,
                    role:role (
                      role_id,
                      role_name,
                      role_image
                    )
                  )
                )
            ";

            return (await _supabase
                .From<RawHeroRow>()
                .Select(columns)
                .Get())
                .Models ?? new List<RawHeroRow>();
        }

        public async Task<RawHeroRow?> GetHeroByID(int heroID)
        {
            const string columns = @"
                hero_id,
                hero_name,
                hero_image,
                hero_role,
                role:role (
                  role_id,
                  role_name,
                  role_image
                ),
                ratings:rating (
                  rating_id,
                  rating,
                  rating_user,
                  player:player (
                    player_id,
                    gamertag,
                    real_name,
                    birthday,
                    native_region,
                    player_image,
                    player_role,
                    current_team,
                    role:role (
                      role_id,
                      role_name,
                      role_image
                    )
                  )
                )
            ";

            return (await _supabase
                .From<RawHeroRow>()
                .Where(h => h.HeroId == heroID)
                .Select(columns)
                .Get())
                .Models
                .FirstOrDefault();
        }
    }
}
