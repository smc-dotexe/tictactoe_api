using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.ViewModels
{
    public class GameBoardViewModel
    {
        public GameBoardViewModel(bool?[,] gameBoard, Guid playerId, string message = null)
        {
            GameBoard = gameBoard;
            PlayerId = playerId;
            Message = message;
        }
        public bool?[,] GameBoard { get; set; }
        public Guid PlayerId { get; set; }
        public string Message { get; set; }
    }
}
