using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Repositories.Interfaces;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.App.Services.Interfaces
{
    public interface IPlayerServices
    {
        Task<GameBoardViewModel> PlaceMove(MoveInputViewModel playerMove);
        List<Player> GeneratePlayers(NewGameViewModel players);
    }
}
