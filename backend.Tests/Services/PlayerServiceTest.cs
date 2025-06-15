using Xunit;
using Moq;
using backend.Services.Implementations;
using backend.Repositories.Interfaces;
using backend.Models;
using backend.Enums;
using backend.Services.Implementations; // Ensure this namespace is correct and matches where PlayerService is defined.
using System.Collections.Generic;

namespace backend.Tests.Services
{
    public class PlayerServiceTests
    {
        private readonly Mock<IPlayerRepository> _mockRepo;
        private readonly PlayerService _service;

        private readonly List<Player> _players = new List<Player>
        {
            new Player { PlayerId = 1, Gamertag = "PlayerOne", NativeRegion = Regions.NA, RealName = "Player One", Birthday = null, PlayerRole = 1, PlayerImage = null, CurrentTeam = null },
            new Player { PlayerId = 2, Gamertag = "PlayerTwo", NativeRegion = Regions.EMEA, RealName = "Player Two", Birthday = null, PlayerRole = 1, PlayerImage = null, CurrentTeam = null },
            new Player { PlayerId = 3, Gamertag = "PlayerThree", NativeRegion = Regions.EMEA, RealName = "Player Three", Birthday = null, PlayerRole = 2, PlayerImage = null, CurrentTeam = null },
            new Player { PlayerId = 4, Gamertag = "PlayerFour", NativeRegion = Regions.Pacific, RealName = "Player Four", Birthday = null, PlayerRole = 2, PlayerImage = null, CurrentTeam = null },
            new Player { PlayerId = 5, Gamertag = "PlayerFive", NativeRegion = Regions.NA, RealName = "Player Five", Birthday = null, PlayerRole = 3, PlayerImage = null, CurrentTeam = null },
            new Player { PlayerId = 6, Gamertag = "PlayerSix", NativeRegion = Regions.NA, RealName = "Player Six", Birthday = null, PlayerRole = 3, PlayerImage = null, CurrentTeam = null },
            new Player { PlayerId = 7, Gamertag = "PlayerSeven", NativeRegion = Regions.Japan, RealName = "Player Seven", Birthday = null, PlayerRole = 1, PlayerImage = null, CurrentTeam = null },
        };  

        public PlayerServiceTests()
        {
            _mockRepo = new Mock<IPlayerRepository>();
            _service = new PlayerService(_mockRepo.Object);
        }

        [Fact]
        public void GetAllPlayers_ReturnsPlayers()
        {
            _mockRepo.Setup(r => r.GetAllPlayers()).Returns(_players);

            // Act
            var result = _service.GetAllPlayers();

            // Assert
            Assert.Equal(result.Count, _players.Count);
            Assert.Equal("PlayerOne", result[0].Gamertag);
            Assert.Equal("PlayerSeven", result[6].Gamertag);
        }
    }
}