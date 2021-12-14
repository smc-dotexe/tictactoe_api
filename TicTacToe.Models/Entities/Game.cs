using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.Models.Entities
{
    public class Game : BaseEntity<Guid>
    {
        private ICollection<Player> _players;
        private ILazyLoader LazyLoader { get; set; }
        public Game() : base() 
        {
            MoveCount = 0;
            MovesLeft = 9;
            GameBoard = new bool?[3, 3];
            IsCompleted = false;
            GamePlayer = new List<GamePlayer>();
        }

        private Game(ILazyLoader lazyLoader)
        {
            LazyLoader = lazyLoader;
        }

        [Required]
        public int MoveCount { get; set; } 
        [Required]
        public int MovesLeft { get; set; }
        [Required]
        public bool?[,] GameBoard { get; set; }
        public bool IsCompleted { get; set; }

        public virtual ICollection<Player> Players 
        { 
            get => LazyLoader.Load(this, ref _players);
            set => _players = value; 
        }

        public virtual ICollection<GamePlayer> GamePlayer { get; set; }

    }
}
