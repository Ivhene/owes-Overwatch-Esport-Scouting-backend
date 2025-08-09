using Xunit;
using Moq;
using backend.Services.Implementations;
using backend.Repositories.Interfaces;
using backend.Models;
using backend.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.DTOs;

namespace backend.Tests.Services
{
    public class PlayerServiceTests
    {
        private readonly Mock<IPlayerRepository> _mockRepo;
        private readonly PlayerService _service;

        private readonly List<Player> _players = new List<Player>
        {
            new Player { PlayerId = 1, Gamertag = "PlayerOne", NativeRegion = Regions.NA, RealName = "Player One", Birthday = null, PlayerRole = 1, PlayerImage = null, CurrentTeam = 1 },
            new Player { PlayerId = 2, Gamertag = "PlayerTwo", NativeRegion = Regions.EMEA, RealName = "Player Two", Birthday = null, PlayerRole = 1, PlayerImage = null, CurrentTeam = 1 },
            new Player { PlayerId = 3, Gamertag = "PlayerThree", NativeRegion = Regions.EMEA, RealName = "Player Three", Birthday = null, PlayerRole = 2, PlayerImage = null, CurrentTeam = 1 },
            new Player { PlayerId = 4, Gamertag = "PlayerFour", NativeRegion = Regions.Pacific, RealName = "Player Four", Birthday = null, PlayerRole = 2, PlayerImage = null, CurrentTeam = 2 },
            new Player { PlayerId = 5, Gamertag = "PlayerFive", NativeRegion = Regions.NA, RealName = "Player Five", Birthday = null, PlayerRole = 3, PlayerImage = null, CurrentTeam = 2 },
            new Player { PlayerId = 6, Gamertag = "PlayerSix", NativeRegion = Regions.NA, RealName = "Player Six", Birthday = null, PlayerRole = 3, PlayerImage = null, CurrentTeam = 2 },
            new Player { PlayerId = 7, Gamertag = "PlayerSeven", NativeRegion = Regions.Japan, RealName = "Player Seven", Birthday = null, PlayerRole = 1, PlayerImage = null, CurrentTeam = 3 },
        };

        public PlayerServiceTests()
        {
            _mockRepo = new Mock<IPlayerRepository>();
            _service = new PlayerService(_mockRepo.Object);
        }

        [Fact]
        public async Task TestGetAllPlayers()
        {
            var completeDtos = _players
            .Select(p => new CompletePlayerDTO
            {
                PlayerId = p.PlayerId,
                Gamertag = p.Gamertag,
                RealName = p.RealName,
                Birthday = p.Birthday,
                NativeRegion = p.NativeRegion,
                PlayerImage = p.PlayerImage,
                Role = new RoleDTO
                {
                    RoleId = p.PlayerRole,
                    RoleName = $"Role{p.PlayerRole}",      
                    RoleImage = $"/images/role{p.PlayerRole}.png"
                },
                CurrentTeam = p.CurrentTeam.HasValue
                    ? new TeamDTO
                    {
                        TeamId = p.CurrentTeam.Value,
                        TeamName = $"Team{p.CurrentTeam.Value}",
                        TeamImage = null,
                        CompetingRegion = p.NativeRegion
                    }
                    : null,
                Ratings = new List<RatingDTO>()
            })
            .ToList();

            _mockRepo.Setup(r => r.GetAllPlayers()).ReturnsAsync(completeDtos);

            List<CompletePlayerDTO> result = await _service.GetAllPlayers();

            Assert.Equal(_players.Count, result.Count);
            Assert.Equal("PlayerOne", result[0].Gamertag);
            Assert.Equal("PlayerSeven", result[6].Gamertag);
        }

        [Fact]
        public async Task TestAddPlayer()
        {
            Player newPlayer = new Player { PlayerId = 8, Gamertag = "PlayerEight", NativeRegion = Regions.Japan, RealName = "Player Eight", Birthday = null, PlayerRole = 1, PlayerImage = null, CurrentTeam = 1 };
            _mockRepo.Setup(r => r.AddPlayer(newPlayer)).ReturnsAsync(newPlayer);

            PlayerDTO result = await _service.AddPlayer(newPlayer);

            Assert.NotNull(result);
            Assert.Equal("PlayerEight", result.Gamertag);
        }

        [Fact]
        public async Task TestDeletePlayer() 
        { 
            _mockRepo.Setup(r => r.DeletePlayer(It.IsAny<int>())).ReturnsAsync(true);
            _mockRepo.Setup(r => r.GetPlayerByID(7)).ThrowsAsync(new KeyNotFoundException("Player not found"));

            Assert.True(await _service.DeletePlayer(7));
            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _service.GetPlayerByID(7));
        }

        [Fact]
        public async Task TestGetPlayerByID()
        {
            var completeDtos = _players
            .Select(p => new CompletePlayerDTO
            {
                PlayerId = p.PlayerId,
                Gamertag = p.Gamertag,
                RealName = p.RealName,
                Birthday = p.Birthday,
                NativeRegion = p.NativeRegion,
                PlayerImage = p.PlayerImage,
                Role = new RoleDTO
                {
                    RoleId = p.PlayerRole,
                    RoleName = $"Role{p.PlayerRole}",
                    RoleImage = $"/images/role{p.PlayerRole}.png"
                },
                CurrentTeam = p.CurrentTeam.HasValue
                    ? new TeamDTO
                    {
                        TeamId = p.CurrentTeam.Value,
                        TeamName = $"Team{p.CurrentTeam.Value}",
                        TeamImage = null,
                        CompetingRegion = p.NativeRegion
                    }
                    : null,
                Ratings = new List<RatingDTO>()
            })
            .ToList();

            int playerId = 1;
            _mockRepo.Setup(r => r.GetPlayerByID(playerId)).ReturnsAsync(completeDtos[0]);

            CompletePlayerDTO result = await _service.GetPlayerByID(playerId);

            Assert.NotNull(result);
            Assert.Equal(playerId, result.PlayerId);
        }

        [Fact]
        public async Task TestGetPlayersByRegion()
        {
            Regions region = Regions.NA;
            var expected = _players.FindAll(p => p.NativeRegion == region);
            _mockRepo.Setup(r => r.GetPlayersByRegion(region)).ReturnsAsync(expected);

            List<PlayerDTO> result = await _service.GetPlayersByRegion(region);

            Assert.NotNull(result);
            Assert.All(result, p => Assert.Equal(region, p.NativeRegion));
        }

        [Fact]
        public async Task TestGetPlayersByTeam()
        {
            int teamId = 1; 
            var expected = _players.FindAll(p => p.CurrentTeam == teamId);
            _mockRepo.Setup(r => r.GetPlayersByTeam(teamId)).ReturnsAsync(expected);

            List<PlayerDTO> result = await _service.GetPlayersByTeam(teamId);

            Assert.NotNull(result);
            Assert.All(result, p => Assert.Equal(teamId, p.CurrentTeam));
        }

        [Fact]
        public async Task TestUpdatePlayer()
        {
            Player updatedPlayer = new Player { PlayerId = 1, Gamertag = "UpdatedPlayerOne", NativeRegion = Regions.NA, RealName = "Updated Player One", Birthday = null, PlayerRole = 1, PlayerImage = null, CurrentTeam = 1 };
            _mockRepo.Setup(r => r.UpdatePlayer(updatedPlayer, 1)).ReturnsAsync(updatedPlayer);

            PlayerDTO result = await _service.UpdatePlayer(updatedPlayer, 1);

            Assert.NotNull(result);
            Assert.Equal("UpdatedPlayerOne", result.Gamertag);
        }
    }
}