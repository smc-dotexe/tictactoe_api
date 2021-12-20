using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.Models.Entities
{
    public class Player : BaseEntity<Guid>
    {
        private Game _game;
        private ILazyLoader LazyLoader;
        public Player() : base() {}

        public Player(PlayerViewModel src)
        {
            Name = src.Name;
            IsTurn = src.IsTurn;
            IsX = src.IsX;
        }

        private Player(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }


        [Required]
        [StringLength(20, ErrorMessage = "Name length must be less than 20 characters")]
        public string Name { get; set; } 
        [Required]
        public bool IsTurn { get; set; }
        [Required]
        public bool IsX { get; set; }
        public virtual Guid GameId { get; set; }
        public virtual Game Game 
        {
            get => LazyLoader.Load(this, ref _game);
            set => _game = value; 
        }

    }
}
