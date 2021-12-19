using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Exceptions;
using TicTacToe.App.Helpers;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;
using Xunit;

namespace TicTacToe.Tests.GameBoardHelperTests
{
    public class GameBoardHelperTests : TestDataHelpers
    {
        
        public GameBoardHelperTests()
        {
            PlayerList = GeneratePlayerData();
            TestGame = new Game() { Id = new Guid("8e3478f6-83c9-44e9-840f-4f3c0b6e98bb"), Players = PlayerList };  
        }

        [Fact]
        public void Player_Inserts_Move_At_First_Index()
        {
            // Arrange
            MoveInputVM = new MoveInputViewModel() { PlayerId = PlayerList[0].Id, Target = new Target(0, 0) };
            GameBoardHelper gameBoardHelper = new GameBoardHelper(TestGame, MoveInputVM.Target, PlayerList[0]);

            // Act
            gameBoardHelper.InsertMove();

            // Assert
            Assert.Equal(true ,gameBoardHelper.gameBoard[0, 0]);
        }

        [Fact]
        public void Wins_By_Row()
        {
            // Arrange
            bool?[,] winByRowMatrix = new bool?[3, 3] { { true, true, null }, { false, false, null }, { null, null, null } };
            Game winByRowGame = new Game() { Id = new Guid("c58b4882-08f6-40d0-abb7-f27df78d3ee0"), Players = PlayerList, GameBoard = winByRowMatrix };
            MoveInputViewModel winningMove = new MoveInputViewModel() { PlayerId = PlayerList[0].Id, Target = new Target(0, 2) };
            GameBoardHelper gameBoardHelper = new GameBoardHelper(winByRowGame, winningMove.Target, PlayerList[0]);

            // Act
            bool? didWin = gameBoardHelper.InsertMove();

            // Assert
            Assert.Equal(true, didWin);
        }

        [Fact]
        public void Wins_By_Diagonal()
        {
            // Arrange
            bool?[,] winByDiagnolArray = new bool?[3, 3] { { true, false, null }, { false, true, null }, { null, null, null } };
            Game winByDiagonalGame = new Game() { Id = new Guid("c58b4882-08f6-40d0-abb7-f27df78d3ee0"), Players = PlayerList, GameBoard = winByDiagnolArray };
            MoveInputViewModel winningMove = new MoveInputViewModel() { PlayerId = PlayerList[0].Id, Target = new Target(2, 2) };
            GameBoardHelper gameBoardHelper = new GameBoardHelper(winByDiagonalGame, winningMove.Target, PlayerList[0]);

            // Act
            bool? didWin = gameBoardHelper.InsertMove();

            // Assert
            Assert.Equal(true, didWin);
        }

        [Fact]
        public void Wins_By_Reverse_Diagonal()
        {
            // Arrange
            bool?[,] winByReverseDiagonalArray = new bool?[3, 3] { { null, false, true }, { false, true, null }, { null, null, null } };
            Game winsByReverseDiagonalGame = new Game() { Id = new Guid("c58b4882-08f6-40d0-abb7-f27df78d3ee0"), Players = PlayerList, GameBoard = winByReverseDiagonalArray };
            MoveInputViewModel winningMove = new MoveInputViewModel() { PlayerId = PlayerList[0].Id, Target = new Target(2, 0) };
            GameBoardHelper gameBoardHelper = new GameBoardHelper(winsByReverseDiagonalGame, winningMove.Target, PlayerList[0]);

            // Act
            bool? didWin = gameBoardHelper.InsertMove();

            Assert.Equal(true, didWin);
        }

        [Fact]
        public void Game_Ends_In_Draw()
        {
            bool?[,] gameDrawArray = new bool?[3, 3] { { true, false, false }, { false, true, true }, { true, true, null } };
            Game gameDraw = new Game() { Id = new Guid("c58b4882-08f6-40d0-abb7-f27df78d3ee0"), Players = PlayerList, GameBoard = gameDrawArray, MoveCount = 7 };
            MoveInputViewModel endMove = new MoveInputViewModel() { PlayerId = PlayerList[0].Id, Target = new Target(2, 2) };
            GameBoardHelper gameBoardHelper = new GameBoardHelper(gameDraw, endMove.Target, PlayerList[0]);

            // Act
            bool? didWin = gameBoardHelper.InsertMove();

            Assert.Null(didWin);

        }

        [Fact]
        public void Wins_By_Column()
        {
            // Arrange
            bool?[,] winByColumnArray = new bool?[3, 3] { { true, false, null }, { true, false, null }, { null, null, null } };
            Game winByColumnGame = new Game() { Id = new Guid("c58b4882-08f6-40d0-abb7-f27df78d3ee0"), Players = PlayerList, GameBoard = winByColumnArray };
            MoveInputViewModel winningMove = new MoveInputViewModel() { PlayerId = PlayerList[0].Id, Target = new Target(2, 0) };
            GameBoardHelper gameBoardHelper = new GameBoardHelper(winByColumnGame, winningMove.Target, PlayerList[0]);

            // Act
            bool? didWin = gameBoardHelper.InsertMove();

            // Assert
            Assert.Equal(true, didWin);
        }

        [Fact]
        public void Exception_Spot_Already_Taken()
        {
            // Arrange
            MoveInputViewModel FirstMoveInput = new MoveInputViewModel() { PlayerId = PlayerList[0].Id, Target = new Target(0, 0) };
            MoveInputViewModel SecondMoveInput = new MoveInputViewModel() { PlayerId = PlayerList[1].Id, Target = new Target(0, 0) };
            GameBoardHelper gameBoardHelper = new GameBoardHelper(TestGame, FirstMoveInput.Target, PlayerList[0]);

            // Act
            gameBoardHelper.InsertMove();
            gameBoardHelper = new GameBoardHelper(TestGame, SecondMoveInput.Target, PlayerList[1]);
            Exception ex = Assert.Throws<SpotTakenException>(() => gameBoardHelper.InsertMove());

            // Assert
            Assert.Equal("Spot already taken", ex.Message);
        }

        [Fact]
        public void Exception_Not_Players_Turn()
        {
            // Arrange
            MoveInputViewModel wrongPlayer = new MoveInputViewModel() { PlayerId = PlayerList[1].Id, Target = new Target(0, 0) };
            GameBoardHelper gameBoardHelper = new GameBoardHelper(TestGame, wrongPlayer.Target, PlayerList[1]);

            // Act
            Exception ex = Assert.Throws<NotPlayersTurnException>(() => gameBoardHelper.InsertMove());

            // Assert
            Assert.Equal("Move invalid: Not players turn", ex.Message);
        }

        [Fact]
        public void Exception_Game_Is_Already_Complete()
        {
            // Arrange
            bool?[,] gameCompletedArray = new bool?[3, 3] { { true, false, null }, { true, false, null }, { true, null, null } };
            Game winByColumnGame = new Game() { Id = new Guid("c58b4882-08f6-40d0-abb7-f27df78d3ee0"), Players = PlayerList, GameBoard = gameCompletedArray, IsCompleted = true, MoveCount = 5 };
            MoveInputViewModel extraMove = new MoveInputViewModel() { PlayerId = PlayerList[0].Id, Target = new Target(2, 2) };
            GameBoardHelper gameBoardHelper = new GameBoardHelper(winByColumnGame, extraMove.Target, PlayerList[0]);

            // Act
            Exception ex = Assert.Throws<GameAlreadyCompletedException>(() => gameBoardHelper.InsertMove());

            // Assert
            Assert.Equal("Game is already completed. Please start a new one", ex.Message);

        }


    }
}
