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
            IsX = src.IsX;
        }

        [Required]
        [StringLength(20, ErrorMessage = "Name length must be less than 20 characters")]
        public string Name { get; set; } 
        [Required]
        public bool IsTurn { get; set; }
        [Required]
        public bool IsX { get; set; }
        public Guid GameId { get; set; }
        public Game Game { get; set; }

    }
}
