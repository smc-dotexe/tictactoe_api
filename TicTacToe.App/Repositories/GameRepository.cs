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
            var player1 = new Player(new PlayerViewModel() { Name = players.PlayerOneName, IsTurn = true, IsFirst=true });
            _playerDbSet.Add(player1);
            var player2 = new Player(new PlayerViewModel() { Name = players.PlayerTwoName, IsTurn = false, IsFirst=false });
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

            bool gameOver = MoveValidation(game, playerMove, player.IsFirst);

            throw new NotImplementedException();
        }

        private bool MoveValidation(Game game, MoveInputViewModel player, bool assignment)
        {
            bool?[,] board = game.GameBoard;
            bool isGameOver = false;
            int row = player.Target.row;
            int col = player.Target.row;
            int dimensionLength = board.GetLength(0);

            if (board[row, col] != null)
            {
                throw new Exception("Spot already taken");
            }

            board[row, col] = assignment;

            game.MoveCount++;

            // Check Row
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[row, i] != assignment)
                    break;

                if (i == dimensionLength - 1)
                    isGameOver = true;
            }

            // Check Columns
            for (int i = 0; i < board.GetLength(1); i++)
            {
                if (board[i, col] != assignment)
                    break;

                if (i == dimensionLength - 1)
                    isGameOver = true;
            }

            // Check Diagonal
            if (row == col)
            {
                for(int i = 0; i < dimensionLength; i++)
                {
                    if (board[i, i] != assignment)
                        break;
                    if (i == dimensionLength - 1)
                        isGameOver = true;
                }
            }

            // Check reverse diagonal
            if (row + col == dimensionLength-1)
            {
                for (int i = 0; i < dimensionLength ; i++)
                {
                    if (board[i, (dimensionLength - 1) - i] != assignment)
                        break;
                    if (i == dimensionLength - 1)
                        isGameOver = true;
                }
            }


            return isGameOver;

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
