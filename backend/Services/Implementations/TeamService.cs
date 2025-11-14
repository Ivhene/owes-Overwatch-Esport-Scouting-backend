using backend.DTOs;
using backend.Models;
using backend.Repositories.Implementations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Services.Implementations
{
    public class TeamService
    {
        private readonly TeamRepository _teamRepository;

        public TeamService(TeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<List<CompleteTeamDTO>> GetAllTeams()
        {
            var raw = await _teamRepository.GetAllTeams();
            return raw.Select(ConvertToCompleteDTO).ToList();
        }

        public async Task<CompleteTeamDTO?> GetTeamByID(int teamID)
        {
            var raw = await _teamRepository.GetTeamByID(teamID);
            return raw == null ? null : ConvertToCompleteDTO(raw);
        }

        public async Task<TeamDTO> AddTeam(Team team)
        {
            var created = await _teamRepository.AddTeam(team);
            return new TeamDTO
            {
                TeamId = created.TeamId,
                TeamName = created.TeamName,
                TeamImage = created.TeamImage,
                CompetingRegion = created.CompetingRegion
            };
        }

        public async Task<TeamDTO?> UpdateTeam(Team team, int teamID)
        {
            var updated = await _teamRepository.UpdateTeam(team, teamID);
            return updated == null ? null : new TeamDTO
            {
                TeamId = updated.TeamId,
                TeamName = updated.TeamName,
                TeamImage = updated.TeamImage,
                CompetingRegion = updated.CompetingRegion
            };
        }

        public Task<bool> DeleteTeam(int teamID)
        {
            return _teamRepository.DeleteTeam(teamID);
        }

        private CompleteTeamDTO ConvertToCompleteDTO(RawTeamRow raw)
        {
            return new CompleteTeamDTO
            {
                TeamId = raw.TeamId,
                TeamName = raw.TeamName,
                TeamImage = raw.TeamImage,
                CompetingRegion = raw.CompetingRegion,
                Players = raw.Players.Select(p => new CompletePlayerDTO
                {
                    PlayerId = p.PlayerId,
                    Gamertag = p.Gamertag,
                    RealName = p.RealName,
                    Birthday = p.Birthday,
                    NativeRegion = p.NativeRegion,
                    PlayerImage = p.PlayerImage,
                    Role = new RoleDTO
                    {
                        RoleId = p.Role.RoleId,
                        RoleName = p.Role.RoleName,
                        RoleImage = p.Role.RoleImage
                    },
                    // We intentionally do not include CurrentTeam here (team context)
                    CurrentTeam = null,
                    Ratings = p.Ratings.Select(rt => new RatingDTO
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
                    }).ToList()
                }).ToList()
            };
        }
    }
}
