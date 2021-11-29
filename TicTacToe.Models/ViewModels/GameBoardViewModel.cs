using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.ViewModels
{
    public class GameBoardViewModel
    {
        public int[,] GameBoard { get; set; }
        public Guid PlayerId { get; set; }

    }
}
