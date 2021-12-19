using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.ViewModels
{
    public class MoveInputViewModel
    {
        public Guid PlayerId { get; set; }
        public Target Target { get; set; }
        
    }
    public class Target
    {
        public Target(int row, int col)
        {
            Row = row;
            Col = col;
        }
        public int Row { get; set; }
        public int Col { get; set; }
    }

}