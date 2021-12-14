using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Models.Entities
{
    public class GamePlayer
    {
        public Guid GameId { get; set; }
        public Game Game { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
    }
}
