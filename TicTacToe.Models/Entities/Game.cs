using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.Models.Entities
{
    public class Game : BaseEntity<Guid>
    {
        public Game() : base() 
        {
            MoveCount = 0;
            MovesLeft = 9;
            GameBoard = new bool?[3, 3];
            IsCompleted = false;
        }

        [Required]
        public int MoveCount { get; set; } 
        [Required]
        public int MovesLeft { get; set; }
        [Required]
        public bool?[,] GameBoard { get; set; }

        public List<Player> Players { get; set; }
        public bool IsCompleted { get; set; }

    }
}
