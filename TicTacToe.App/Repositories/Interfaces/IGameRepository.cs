using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.App.Repositories.Interfaces
{
    public interface IGameRepository
    {
        Task<GameViewModel> StartNewGame(NewGameViewModel newGame);
        Task<GameBoardViewModel> PlaceMove(MoveInputViewModel playerMove);
    }
}
