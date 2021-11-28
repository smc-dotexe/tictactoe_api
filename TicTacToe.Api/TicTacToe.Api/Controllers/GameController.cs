using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.App.Repositories.Interfaces;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;

        public GameController(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        [HttpPost("newgame")]
        public async Task<ActionResult<GameViewModel>> NewGame([FromBody] NewGameViewModel startGame)
        {
            Console.WriteLine("FROM BODY " + startGame);
            var gameStarted = await _gameRepository.StartNewGame(startGame);
            Console.WriteLine("GAMESTARTED = " + gameStarted);
            return Ok(gameStarted);
        }
    }
}
