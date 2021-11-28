using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.Entities;

namespace TicTacToe.Models.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel(Game gameSrc, Player playerOneSrc, Player playerTwoSrc)
        {
            GameId = gameSrc.Id;
            PlayerOneId = playerOneSrc.Id;
            PlayerTwoId = playerTwoSrc.Id;
        }
        public Guid GameId { get; set; }
        public Guid PlayerOneId { get; set; }
        public Guid PlayerTwoId { get; set; }
    }
}
