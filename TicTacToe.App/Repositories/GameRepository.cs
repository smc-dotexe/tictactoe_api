using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Repositories.Interfaces;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.App.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private DbSet<Game> _gameDbSet;
        private DbSet<Player> _playerDbSet;
        public GameRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _gameDbSet = dbContext.Set<Game>();
            _playerDbSet = dbContext.Set<Player>();
        }
        public async Task<GameViewModel> StartNewGame(NewGameViewModel newGame)
        {

            var player1 = new Player(new PlayerViewModel() { Name = newGame.PlayerOneName, IsTurn = true });
            _playerDbSet.Add(player1);
            var player2= new Player(new PlayerViewModel() { Name = newGame.PlayerTwoName, IsTurn = false });
            _playerDbSet.Add(player2);
            var game = new Game();
            _gameDbSet.Add(game);
            List<Player> playerList = new List<Player>() { player1, player2 };
            game.Players = playerList;
            player1.Game = game;
            player1.GameId = game.Id;
            player2.Game = game;
            player2.GameId = game.Id;

            var generatedGame = new GameViewModel(game,player1, player2);

            await _dbContext.SaveChangesAsync();

            return generatedGame;


        }
    }
}
