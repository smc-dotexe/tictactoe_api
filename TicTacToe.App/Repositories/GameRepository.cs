using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Repositories.Interfaces;
using TicTacToe.App.Services;
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
            var player1 = new Player(new PlayerViewModel() { Name = players.PlayerOneName, IsTurn = true, IsX=true });
            _playerDbSet.Add(player1);
            var player2 = new Player(new PlayerViewModel() { Name = players.PlayerTwoName, IsTurn = false, IsX=false });
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

        public async Task<GameBoardViewModel> PlaceMove(MoveInputViewModel playerMove)
        {
            Player player = await _playerDbSet.FindAsync(playerMove.PlayerId);
            Game game = await _gameDbSet.FindAsync(player.GameId);

            GameBoardValidation(player, game);

            var boardService = new GameBoardService(game, playerMove.Target, player.IsX);
            boardService.InsertMove();
            boardService.CheckIfWin();
            await _dbContext.SaveChangesAsync();

            return new GameBoardViewModel(boardService.gameBoard, player.Id, boardService.visualBoard);
        }

        private void GameBoardValidation(Player player, Game game)
        {
            if (game.IsCompleted)
            {
                throw new Exception("Game is already completed. Please start a new one");
            }
            if (player.IsTurn)
            {
                throw new Exception("Move invalid: Not players turn");
            }
        }
    }
}
