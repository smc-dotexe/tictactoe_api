using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Models.Entities;

namespace TicTacToe.App.Services
{
    public class GameBoardService
    {
        public Game game;
        public bool?[,] gameBoard;
        public bool isGameOver;
        public string visualBoard;
        int _dimensionLength;
        int _row;
        int _col;
        bool _isX;

        public GameBoardService(Game userGame, (int row, int col) userPlacement, bool isX)
        {
            game = userGame;
            gameBoard = userGame.GameBoard;
            _dimensionLength = userGame.GameBoard.GetLength(0);
            _row = userPlacement.row;
            _col = userPlacement.col;
            _isX = isX;
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
            if (game.MoveCount == Math.Pow(_dimensionLength, 2) -1)
            {
                isGameOver = true;
            }

            return isGameOver;
        }

        public void PrintBoard()
        {
            for (int r = 0; r < gameBoard.GetLength(0); r++)
            {
                visualBoard.Concat("\n");
                for (int c = 0; c < gameBoard.GetLength(1); c++)
                {
                    visualBoard.Concat(gameBoard[r, c].ToString());
                }
                visualBoard.Concat("\n");
                visualBoard.Concat("\n");
            }
        }


    }
}
