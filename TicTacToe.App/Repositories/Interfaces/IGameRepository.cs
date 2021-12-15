using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.Dtos;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.App.Repositories.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game, Guid>
    {
        public Task<GameDto> GetGameByPlayerId(Guid playerId);
        public Task UpdateGameAndPlayers(GameDto game);
    }
}
