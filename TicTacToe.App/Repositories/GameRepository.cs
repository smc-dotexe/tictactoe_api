using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.App.Repositories.Interfaces;
using TicTacToe.App.Services;
using TicTacToe.Models.Dtos;
using TicTacToe.Models.Entities;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.App.Repositories
{
    public class GameRepository : BaseRepository<Game, Guid, ApplicationDbContext>, IGameRepository
    {
        //private readonly ApplicationDbContext _dbContext;
        //private DbSet<Game> _gameDbSet;
        //private DbSet<Player> _playerDbSet;
        //private DbSet<GamePlayer> _gamePlayerDbSet;
        public GameRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            //_dbContext = dbContext;
            //_gameDbSet = dbContext.Set<Game>();
            //_playerDbSet = dbContext.Set<Player>();
            //_gamePlayerDbSet = dbContext.Set<GamePlayer>();
        }

        public async Task<GameDto> GetGameByPlayerId(Guid playerId)
        {
            Game game = await _entityDbSet.FirstOrDefaultAsync(g => g.Players.Any(p => p.Id == playerId));
            Player currentPlayer = game.Players.FirstOrDefault(player => player.Id == playerId);
            Player nextPlayer = game.Players.FirstOrDefault(player => player.Id != playerId);

            return new GameDto(game, currentPlayer, nextPlayer);
        }

        public async Task UpdateGameAndPlayers(GameDto game)
        {
            _context.Update(game.CurrentPlayer);
            _context.Update(game.NextPlayer);
            _context.Update(game.Game);

            await _context.SaveChangesAsync();
        }
        //public async Task<GameViewModel> StartNewGame(NewGameViewModel newGame)
        //{
        //    List<Player> playerList = GeneratePlayers(newGame);

        //    GameViewModel generatedGame = GenerateGame(playerList);

        //    await _dbContext.SaveChangesAsync();

        //    return generatedGame;
        //}

        //private List<Player> GeneratePlayers(NewGameViewModel players)
        //{
        //    var player1 = new Player(new PlayerViewModel() { Name = players.PlayerOneName, IsTurn = true, IsX=true });
        //    //_playerDbSet.Add(player1);
        //    var player2 = new Player(new PlayerViewModel() { Name = players.PlayerTwoName, IsTurn = false, IsX=false });
        //    //_playerDbSet.Add(player2);

        //    return new List<Player>() { player1, player2 };
        //}


        //private GameViewModel GenerateGame(List<Player> playerList)
        //{
        //    var game = new Game();

        //    game.Players = playerList;

        //    foreach (var player in playerList)
        //    {
        //        player.Game = game;
        //        player.GameId = game.Id;
        //        _playerDbSet.Add(player);
        //        game.GamePlayer.Add(new GamePlayer() { GameId = game.Id, PlayerId = player.Id, Player = player });
        //    }

        //    _gameDbSet.Add(game);
        //    return new GameViewModel(game, playerList[0], playerList[1]);
        //}

        //public async Task<GameBoardViewModel> PlaceMove(MoveInputViewModel playerMove)
        //{

        //    Game game = await _gameDbSet.FirstOrDefaultAsync(g => g.Players.Any(p => p.Id == playerMove.PlayerId));

        //    Player currentPlayer = game.Players.FirstOrDefault(player => player.Id == playerMove.PlayerId);
        //    Player nextPlayer = game.Players.FirstOrDefault(player => player.Id != playerMove.PlayerId);
        //    // Insure that it is the player's turn and the game is not completed
        //    GameBoardValidation(currentPlayer, game);


        //    var boardService = new GameBoardService(game, playerMove.Target, currentPlayer.IsX);
        //    boardService.InsertMove();
        //    boardService.CheckIfWin();

        //    currentPlayer.IsTurn = !currentPlayer.IsTurn;
        //    await _dbContext.SaveChangesAsync();

        //    return new GameBoardViewModel(boardService.gameBoard, currentPlayer.Id, boardService.visualBoard);
        //    //return null;
        //}


    }
}
