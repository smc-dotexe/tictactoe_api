using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            List<Player> playerList = GeneratePlayers(newGame);

            GameViewModel generatedGame = GenerateGame(playerList);

            await _dbContext.SaveChangesAsync();

            return generatedGame;
        }

        private List<Player> GeneratePlayers(NewGameViewModel players)
        {
            var player1 = new Player(new PlayerViewModel() { Name = players.PlayerOneName, IsTurn = true });
            _playerDbSet.Add(player1);
            var player2 = new Player(new PlayerViewModel() { Name = players.PlayerTwoName, IsTurn = false });
            _playerDbSet.Add(player2);

            return new List<Player>() { player1, player2 };
        }

        
        private GameViewModel GenerateGame(List<Player> playerList)
        {
            var game = new Game();
            _gameDbSet.Add(game);

            game.Players = playerList;

            foreach (var player in playerList)
            {
                player.Game = game;
                player.GameId = game.Id;
            }

            return new GameViewModel(game, playerList[0], playerList[1]);
        }
    }
}
