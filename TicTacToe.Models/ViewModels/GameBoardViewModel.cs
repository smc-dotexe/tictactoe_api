using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.ViewModels
{
    public class GameBoardViewModel
    {
        public GameBoardViewModel(bool?[,] gameBoard, Guid playerId)
        {
            GameBoard = gameBoard;
            PlayerId = playerId;
        }
        public bool?[,] GameBoard { get; set; }
        public Guid PlayerId { get; set; }
    }
}
