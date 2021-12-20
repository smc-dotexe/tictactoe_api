using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Exceptions;
using TicTacToe.App.Repositories.Interfaces;
using TicTacToe.App.Services;
using TicTacToe.Models.Dtos;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.App.Repositories
{
    public class GameRepository : BaseRepository<Game, Guid, ApplicationDbContext>, IGameRepository
    {
        public GameRepository(ApplicationDbContext dbContext) : base(dbContext) {}

        public async Task<GameDto> GetGameByPlayerId(Guid playerId)
        {
            Game game = await _entityDbSet.FirstOrDefaultAsync(g => g.Players.Any(p => p.Id == playerId));

            if (game == null)
                throw new NotFoundException("Player was not found");

            Player currentPlayer = game.Players.FirstOrDefault(player => player.Id == playerId);
            Player nextPlayer = game.Players.FirstOrDefault(player => player.Id != playerId);

            return new GameDto(game, currentPlayer, nextPlayer);
        }

        public async Task<List<Game>> GetAllActiveGames()
        {
            List<Game> activeGamesQuery = await _context.Games.Where(g => g.IsCompleted == false).ToListAsync();

            return activeGamesQuery;
        }
    }
}
