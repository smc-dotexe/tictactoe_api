using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.ViewModels
{
    public class ActiveGameViewModel
    {
        public Guid GameId { get; set; }
        public int MovesTaken { get; set; }
        public string PlayerOneName { get; set; }
        public string PlayerTwoName { get; set; }

    }
}
