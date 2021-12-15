using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Exceptions;
using TicTacToe.App.Helpers;
using TicTacToe.App.Repositories.Interfaces;
using TicTacToe.App.Services.Interfaces;
using TicTacToe.Models.Dtos;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.App.Services
{
    public class PlayerServices : IPlayerServices
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IGameRepository _gameRepository;
        //private readonly IGameServices _gameServices;

        public PlayerServices(IPlayerRepository playerRepository, IGameRepository gameRepository)
        {
            _playerRepository = playerRepository;
            _gameRepository = gameRepository;
            //_gameServices = gameServices;
        }

        public List<Player> GeneratePlayers(NewGameViewModel players)
        {
            // Doesn't save to the database yet. Returns players list to GameServices to get a reference to the gameId
            var player1 = new Player(new PlayerViewModel() { Name = players.PlayerOneName, IsTurn = true, IsX = true });
            var player2 = new Player(new PlayerViewModel() { Name = players.PlayerTwoName, IsTurn = false, IsX = false });

            return new List<Player>() { player1, player2 };
        }

        public async Task<GameBoardViewModel> PlaceMove(MoveInputViewModel playerMove)
        {
            GameBoardViewModel gameBoardVM;
            GameDto game = await _gameRepository.GetGameByPlayerId(playerMove.PlayerId);
            GameBoardHelper boardHelper = new GameBoardHelper(game.Game, playerMove.Target, game.CurrentPlayer);
            string message;

            boardHelper.GameBoardValidation();
            boardHelper.InsertMove();
            bool didWin = boardHelper.CheckIfWin();

            if (didWin)
            { 
                message = game.CurrentPlayer.Name + " wins!";
                game.Game.IsCompleted = true;
                gameBoardVM = new GameBoardViewModel(boardHelper.gameBoard, game.CurrentPlayer.Id, message);
            }
            else
            {
                game.CurrentPlayer.IsTurn = !game.CurrentPlayer.IsTurn;
                game.NextPlayer.IsTurn = !game.NextPlayer.IsTurn;
                message = game.CurrentPlayer.Name + "'s turn is complete, " + game.NextPlayer.Name + "'s turn next";
                gameBoardVM = new GameBoardViewModel(boardHelper.gameBoard, game.CurrentPlayer.Id, message);
            }

            await _gameRepository.UpdateGameAndPlayers(game);

            return gameBoardVM;
        }
    }
}
