using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.Models.Entities
{
    public class Player : BaseEntity<Guid>
    {
        public Player() : base() {}

        public Player(PlayerViewModel src)
        {
            Name = src.Name;
            IsTurn = src.IsTurn;
        }

        [Required]
        public string Name { get; set; } 
        [Required]
        public bool IsTurn { get; set; }

        public Guid GameId { get; set; }
        public Game Game { get; set; }

    }
}
