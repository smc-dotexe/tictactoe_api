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
        public (int row, int column) Target { get; set; }
    }
}
