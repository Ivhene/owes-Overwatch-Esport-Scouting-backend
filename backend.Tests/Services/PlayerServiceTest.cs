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
        public void TestGetAllPlayers()
        {
            _mockRepo.Setup(r => r.GetAllPlayers()).Returns(_players);

            List<Player> result = _service.GetAllPlayers();

            Assert.Equal(result.Count, _players.Count);
            Assert.Equal("PlayerOne", result[0].Gamertag);
            Assert.Equal("PlayerSeven", result[6].Gamertag);
        }

        [Fact]
        public void TestAddPlayer()
        {
            Player newPlayer = new Player { PlayerId = 8, Gamertag = "PlayerEight", NativeRegion = Regions.Japan, RealName = "Player Eight", Birthday = null, PlayerRole = 1, PlayerImage = null, CurrentTeam = 1 };
            _mockRepo.Setup(r => r.AddPlayer(newPlayer)).Returns(newPlayer);

            Player result = _service.AddPlayer(newPlayer);

            Assert.NotNull(result);
            Assert.Equal("PlayerEight", result.Gamertag);
        }

        [Fact]
        public void TestDeletePlayer() 
        { 
            _mockRepo.Setup(r => r.DeletePlayer(It.IsAny<int>())).Returns(true);
            _mockRepo.Setup(r => r.GetPlayerByID(7)).Throws(new KeyNotFoundException("Player not found"));

            Assert.True(_service.DeletePlayer(7));
            Assert.Throws<KeyNotFoundException>(() => _service.GetPlayerByID(7));
        }

        [Fact]
        public void TestGetPlayerByGamertag()
        {
            string gamertag = "PlayerThree";
            _mockRepo.Setup(r => r.GetPlayerByGamertag(gamertag)).Returns(_players[2]);

            Player result = _service.GetPlayerByGamertag(gamertag);

            Assert.NotNull(result);
            Assert.Equal(gamertag, result.Gamertag);
        }

        [Fact]
        public void TestGetPlayerByID()
        {
            int playerId = 1;
            _mockRepo.Setup(r => r.GetPlayerByID(playerId)).Returns(_players[0]);

            Player result = _service.GetPlayerByID(playerId);

            Assert.NotNull(result);
            Assert.Equal(playerId, result.PlayerId);
        }

        [Fact]
        public void TestGetPlayersByRegion()
        {
            Regions region = Regions.NA;
            _mockRepo.Setup(r => r.GetPlayersByRegion(region)).Returns(_players.FindAll(p => p.NativeRegion == region));

            List<Player> result = _service.GetPlayersByRegion(region);

            Assert.NotNull(result);
            Assert.All(result, p => Assert.Equal(region, p.NativeRegion));
        }

        [Fact]
        public void TestGetPlayersByTeam()
        {
            int teamId = 1; 
            _mockRepo.Setup(r => r.GetPlayersByTeam(teamId)).Returns(_players.FindAll(p => p.CurrentTeam == teamId));

            List<Player> result = _service.GetPlayersByTeam(teamId);

            Assert.NotNull(result);
            Assert.All(result, p => Assert.Equal(teamId, p.CurrentTeam));
        }

        [Fact]
        public void TestUpdatePlayer()
        {
            Player updatedPlayer = new Player { PlayerId = 1, Gamertag = "UpdatedPlayerOne", NativeRegion = Regions.NA, RealName = "Updated Player One", Birthday = null, PlayerRole = 1, PlayerImage = null, CurrentTeam = 1 };
            _mockRepo.Setup(r => r.UpdatePlayer(updatedPlayer)).Returns(updatedPlayer);

            Player result = _service.UpdatePlayer(updatedPlayer);

            Assert.NotNull(result);
            Assert.Equal("UpdatedPlayerOne", result.Gamertag);
        }
    }
}