using backend.DTOs;
using backend.Repositories.Implementations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Implementations
{
    public class HeroService
    {
        private readonly HeroRepository _heroRepository;

        public HeroService(HeroRepository heroRepository)
        {
            _heroRepository = heroRepository;
        }

        public async Task<List<CompleteHeroDTO>> GetAllHeroes()
        {
            var raw = await _heroRepository.GetAllHeroes();
            return raw.Select(ConvertToCompleteDTO).ToList();
        }

        public async Task<CompleteHeroDTO?> GetHeroByID(int heroID)
        {
            var raw = await _heroRepository.GetHeroByID(heroID);
            return raw == null ? null : ConvertToCompleteDTO(raw);
        }

        private CompleteHeroDTO ConvertToCompleteDTO(RawHeroRow raw)
        {
            return new CompleteHeroDTO
            {
                HeroId = raw.HeroId,
                HeroName = raw.HeroName,
                HeroImage = raw.HeroImage,
                Role = raw.Role == null
                    ? new RoleDTO { RoleId = raw.HeroRole, RoleName = null!, RoleImage = null! }
                    : new RoleDTO
                    {
                        RoleId = raw.Role.RoleId,
                        RoleName = raw.Role.RoleName,
                        RoleImage = raw.Role.RoleImage
                    },
                Ratings = raw.Ratings.Select(r => new RatingWithPlayerDTO
                {
                    RatingId = r.RatingId,
                    Ratings = r.Ratings,
                    RatingUser = r.RatingUser,
                    Player = new PlayerDTO
                    {
                        PlayerId = r.Player.PlayerId,
                        Gamertag = r.Player.Gamertag,
                        RealName = r.Player.RealName,
                        Birthday = r.Player.Birthday,
                        NativeRegion = r.Player.NativeRegion,
                        PlayerImage = r.Player.PlayerImage,
                        CurrentTeam = r.Player.CurrentTeam
                    }
                }).ToList()
            };
        }
    }
}
