using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Repositories.Interfaces;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.Tests
{
    public class TestDataHelpers
    {
        protected bool?[,] GameBoard = new bool?[3,3];
        protected NewGameViewModel NewGameVM;
        protected GameViewModel TestGameVM;
        protected List<Player> PlayerList;
        protected Game TestGame;
        protected MoveInputViewModel MoveInputVM;
        protected GameBoardViewModel TestGameBoardVM;
        protected Mock MockPlayerRepository = new Mock<IPlayerRepository>();
        protected Mock MockGameRepository = new Mock<IGameRepository>();
        protected static List<Player> GeneratePlayerData()
        {
            Player playerOne = new Player() { Id = new Guid("b5cd2f90-2a48-4afe-b494-96d89b1ee9bb"), Name = "FirstPlayer", IsTurn = true, IsX = true };
            Player playerTwo = new Player() { Id = new Guid("6d1e3c04-3414-4286-a5c7-cdbcd5c1c75e"), Name = "SecondPlayer", IsTurn = false, IsX = false };

            return new List<Player>() { playerOne, playerTwo };
        }
    }
}
