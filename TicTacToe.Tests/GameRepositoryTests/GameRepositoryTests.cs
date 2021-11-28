using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App;
using TicTacToe.App.Repositories.Interfaces;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;
using Xunit;

namespace TicTacToe.Tests.GameRepositoryTests
{
    public class GameRepositoryTests
    {
        private readonly NewGameViewModel _newGameVm;
        private readonly GameViewModel _gameVm;
        private readonly List<Player> _playerList;
        private readonly Game _game;

        public GameRepositoryTests()
        {
            _playerList = GeneratePlayerData();
            _game = new Game() { Id = new Guid("8e3478f6-83c9-44e9-840f-4f3c0b6e98bb"), Players = _playerList };
            _gameVm = new GameViewModel(_game, _playerList[0], _playerList[1]);
            _newGameVm = new NewGameViewModel() { PlayerOneName = "FirstPlayer", PlayerTwoName = "SecondPlayer" };
        }

        [Fact]
        public async Task StartNewGame_Returns_GameViewModel_With_Matching_Ids()
        {
            // Arrange 
            var mockGameRepo = new Mock<IGameRepository>();
            mockGameRepo.Setup(repo => repo.StartNewGame(_newGameVm))
                .ReturnsAsync(_gameVm);

            // Act
            var result = await mockGameRepo.Object.StartNewGame(_newGameVm);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<GameViewModel>(result);
            Assert.Equal(new Guid("8e3478f6-83c9-44e9-840f-4f3c0b6e98bb"), result.GameId);
            Assert.Equal(new Guid("b5cd2f90-2a48-4afe-b494-96d89b1ee9bb"), result.PlayerOneId);
            Assert.Equal(new Guid("6d1e3c04-3414-4286-a5c7-cdbcd5c1c75e"), result.PlayerTwoId);
        }

        private static List<Player> GeneratePlayerData()
        {
            Player playerOne = new Player() { Id = new Guid("b5cd2f90-2a48-4afe-b494-96d89b1ee9bb"), Name = "FirstPlayer", IsTurn = true };
            Player playerTwo = new Player() { Id = new Guid("6d1e3c04-3414-4286-a5c7-cdbcd5c1c75e"), Name = "SecondPlayer", IsTurn = false };

            return new List<Player>() { playerOne, playerTwo };
        }

    }
}
