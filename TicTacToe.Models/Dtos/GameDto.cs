using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.Entities;

namespace TicTacToe.Models.Dtos
{
    public class GameDto
    {
        public GameDto(Game game, Player currentPlayer, Player nextPlayer)
        {
            Game = game;
            CurrentPlayer = currentPlayer;
            NextPlayer = nextPlayer;
        }
        public Game Game { get; set; }
        public Player CurrentPlayer { get; set; }
        public Player NextPlayer { get; set; }
    }
}
