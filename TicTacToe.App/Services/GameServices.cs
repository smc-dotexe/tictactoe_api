using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Repositories.Interfaces;
using TicTacToe.App.Services.Interfaces;
using TicTacToe.Models.Dtos;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.App.Services
{
    public class GameServices : IGameServices
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPlayerServices _playerServices;

        public GameServices(IGameRepository gameRepository, IPlayerServices playerServices)
        {
            _gameRepository = gameRepository;
            _playerServices = playerServices;
        }

        public async Task<GameViewModel> StartNewGame(NewGameViewModel newGame)
        {
            List<Player> playerList = _playerServices.GeneratePlayers(newGame);
            GameViewModel generatedGame = await GenerateGame(playerList);

            return generatedGame;
        }

        public async Task<GameViewModel> GenerateGame(List<Player> playerList)
        {
            var game = new Game
            {
                Players = playerList
            };

            foreach (var player in playerList)
            {
                player.Game = game;
                player.GameId = game.Id;
            }

            await _gameRepository.Create(game);
            return new GameViewModel(game, playerList[0], playerList[1]);
        }

        public async Task<List<ActiveGameViewModel>> GetActiveGames()
        {
            List<Game> currentGamesList = await _gameRepository.GetAllActiveGames();
            if (currentGamesList.Count == 0)
                return null;

            List<ActiveGameViewModel> activeGamesListView = new List<ActiveGameViewModel>();

            foreach(Game game in currentGamesList)
            {
                ActiveGameViewModel activeGame = new ActiveGameViewModel() 
                { 
                    GameId = game.Id, 
                    MovesTaken = game.MoveCount, 
                    PlayerOneName = game.Players.First().Name, 
                    PlayerTwoName = game.Players.Last().Name 
                };

                activeGamesListView.Add(activeGame);
            }

            return activeGamesListView;
        }
    }
}
