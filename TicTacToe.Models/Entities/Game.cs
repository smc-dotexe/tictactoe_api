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
            GameBoard = new List<string>() { "0", "1", "2", "3", "4", "5", "6", "7", "8" };
            IsCompleted = false;
        }

        [Required]
        public int MoveCount { get; set; } 
        [Required]
        public int MovesLeft { get; set; }
        [Required]
        public List<string> GameBoard { get; set; }

        public List<Player> Players { get; set; }
        public bool IsCompleted { get; set; }

    }
}
