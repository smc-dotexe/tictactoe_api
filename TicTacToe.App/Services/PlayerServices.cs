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
        private readonly IGameRepository _gameRepository;
        public GameBoardViewModel gameBoardVM;
        public GameDto gameDto;
        public GameBoardHelper boardHelper;
        public string gameMessage;
        public PlayerServices(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
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
            gameDto = await _gameRepository.GetGameByPlayerId(playerMove.PlayerId);
            boardHelper = new GameBoardHelper(gameDto.Game, playerMove.Target, gameDto.CurrentPlayer);

            bool? didWin = boardHelper.InsertMove();

            UpdateModelsBasedOnMovePlacement(didWin);

            await _gameRepository.Update(gameDto.Game);

            return gameBoardVM;
        }

        private void UpdateModelsBasedOnMovePlacement(bool? didWin)
        {
            if (didWin == true)
            {
                gameMessage = gameDto.CurrentPlayer.Name + " wins!";
                gameDto.Game.IsCompleted = true;
                gameBoardVM = new GameBoardViewModel(boardHelper.gameBoard, gameDto.CurrentPlayer.Id, gameMessage);
            }

            if (didWin == false)
            {
                gameDto.CurrentPlayer.IsTurn = !gameDto.CurrentPlayer.IsTurn;
                gameDto.NextPlayer.IsTurn = !gameDto.NextPlayer.IsTurn;

                gameMessage = gameDto.CurrentPlayer.Name + "'s turn is complete, " + gameDto.NextPlayer.Name + "'s turn next";
                gameBoardVM = new GameBoardViewModel(boardHelper.gameBoard, gameDto.CurrentPlayer.Id, gameMessage);
            }

            if (didWin == null)
            {
                gameMessage = "Game was a draw";
                gameDto.Game.IsCompleted = true;
                gameBoardVM = new GameBoardViewModel(boardHelper.gameBoard, gameDto.CurrentPlayer.Id, gameMessage);
            }
        }
    }
}
