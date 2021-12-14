using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.Entities;

namespace TicTacToe.App.Helpers
{
    public class GameBoardHelper
    {
        public Game game;
        public Player player;
        public bool?[,] gameBoard;
        public bool isGameOver;
        public string visualBoard;
        int _dimensionLength;
        int _row;
        int _col;
        bool _isX;

        public GameBoardHelper(Game userGame, (int row, int col) userPlacement, Player currentPlayer)
        {
            game = userGame;
            player = currentPlayer;
            gameBoard = userGame.GameBoard;
            _dimensionLength = userGame.GameBoard.GetLength(0);
            _row = userPlacement.row;
            _col = userPlacement.col;
            _isX = player.IsX;
        }

        public void InsertMove()
        {
            if (gameBoard[_row, _col] != null)
            {
                throw new Exception("Spot already taken");
            }

            gameBoard[_row, _col] = _isX;
            game.MoveCount++;
        }

        public bool CheckIfWin()
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
                isGameOver = true;
            }

            PrintBoard();
            return isGameOver;
        }

        public void PrintBoard()
        {
            for (int r = 0; r < gameBoard.GetLength(0); r++)
            {
                visualBoard += "\n";
                for (int c = 0; c < gameBoard.GetLength(1); c++)
                {
                    visualBoard += gameBoard[r, c].ToString();
                }
                visualBoard += visualBoard + "\n";
                visualBoard += visualBoard + "\n";
            }
        }
        public void GameBoardValidation()
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
