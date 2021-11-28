using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.ViewModels
{
    public class PlayerViewModel
    {
        public Guid PlayerId { get; set; }
        public string Name { get; set; }
        public bool IsTurn { get; set; }
    }
}
