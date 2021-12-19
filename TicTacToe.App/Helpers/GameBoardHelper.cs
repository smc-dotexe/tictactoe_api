using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Exceptions;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.App.Helpers
{
    public class GameBoardHelper
    {
        public Game game;
        public Player player;
        public bool?[,] gameBoard;
        public bool? isGameOver = false;
        public string visualBoard;
        int _dimensionLength;
        int _row;
        int _col;
        bool _isX;

        public GameBoardHelper(Game userGame, Target userPlacement, Player currentPlayer)
        {
            game = userGame;
            player = currentPlayer;
            gameBoard = userGame.GameBoard;
            _dimensionLength = userGame.GameBoard.GetLength(0);
            _row = userPlacement.Row;
            _col = userPlacement.Col;
            _isX = player.IsX;
        }

        public bool? InsertMove()
        {
            GameBoardValidation();

            gameBoard[_row, _col] = _isX;
            game.MoveCount++;

            bool? checkWin = CheckIfWin();
            return checkWin;
        }

        private bool? CheckIfWin()
        {
            // Checking Rows
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                if (gameBoard[_row, i] != _isX)
                    break;

                if (i == _dimensionLength - 1)
                    isGameOver = true;
            }

            // Check Columns
            for (int i = 0; i < gameBoard.GetLength(1); i++)
            {
                if (gameBoard[i, _col] != _isX)
                    break;

                if (i == _dimensionLength - 1)
                    isGameOver = true;
            }

            // Check Diagonal
            if (_row == _col)
            {
                for (int i = 0; i < _dimensionLength; i++)
                {
                    if (gameBoard[i, i] != _isX)
                        break;
                    if (i == _dimensionLength - 1)
                        isGameOver = true;
                }
            }

            // Check reverse diagonal
            if (_row + _col == _dimensionLength - 1)
            {
                for (int i = 0; i < _dimensionLength; i++)
                {
                    if (gameBoard[i, (_dimensionLength - 1) - i] != _isX)
                        break;
                    if (i == _dimensionLength - 1)
                        isGameOver = true;
                }
            }

            // Check for a draw
            if (game.MoveCount == Math.Pow(_dimensionLength, 2) - 1)
            {
                isGameOver = null;
            }

            return isGameOver;
        }
        private void GameBoardValidation()
        {
            if (gameBoard[_row, _col] != null)
                throw new SpotTakenException("Spot already taken");
 
            if (game.IsCompleted)
                throw new GameAlreadyCompletedException("Game is already completed. Please start a new one");

            if (!player.IsTurn)
                throw new NotPlayersTurnException("Move invalid: Not players turn");
        }
    }
}
