using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Services.Interfaces;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;
using Xunit;

namespace TicTacToe.Tests.GameServiceTests
{
    public class GameServiceTests : TestDataHelpers
    {
        public GameServiceTests()
        {
            PlayerList = GeneratePlayerData();
            TestGame = new Game() { Id = new Guid("8e3478f6-83c9-44e9-840f-4f3c0b6e98bb"), Players = PlayerList };
            TestGameVM = new GameViewModel(TestGame, PlayerList[0], PlayerList[1]);
            NewGameVM = new NewGameViewModel() { PlayerOneName = "FirstPlayer", PlayerTwoName = "SecondPlayer" };
        }

        [Fact]
        public async Task StartNewGame_Returns_GameViewModel_With_Matching_Ids()
        {
            // Arrange 
            var mockGameService = new Mock<IGameServices>();
            mockGameService.Setup(serv => serv.StartNewGame(NewGameVM))
                .ReturnsAsync(TestGameVM);

            // Act
            var result = await mockGameService.Object.StartNewGame(NewGameVM);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GameViewModel>(result);
            Assert.Equal(new Guid("8e3478f6-83c9-44e9-840f-4f3c0b6e98bb"), result.GameId);
            Assert.Equal(new Guid("b5cd2f90-2a48-4afe-b494-96d89b1ee9bb"), result.PlayerOneId);
            Assert.Equal(new Guid("6d1e3c04-3414-4286-a5c7-cdbcd5c1c75e"), result.PlayerTwoId);
        }
    }
}
