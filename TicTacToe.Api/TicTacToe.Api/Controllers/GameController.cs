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
            if (!ModelState.IsValid)
                return BadRequest();

            var gameStarted = await _gameRepository.StartNewGame(startGame);
            return Ok(gameStarted);
        }
    }
}
