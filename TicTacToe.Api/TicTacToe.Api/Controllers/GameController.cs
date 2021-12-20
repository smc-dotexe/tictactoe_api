using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.App.Services.Interfaces;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameServices _gameServices;

        public GameController(IGameServices gameServices)
        {
            _gameServices = gameServices;
        }
        
        /// <summary>
        /// Generates a new game with two players
        /// </summary>
        /// <param name="startGame"></param>
        /// <returns>Game Id and both Player Ids</returns>
        /// <remarks>
        /// Sample Request:
        ///     POST
        ///     {
        ///         "playerOneName": "sample name",
        ///         "playerTwoName": "sample name2"
        ///     }
        /// </remarks>
        /// <response code="200">Returns the ids of the new game and players</response>
        /// <response code="400">If player names are >= 20 characters or if the value is null</response>
        [HttpPost("newgame")]
        public async Task<ActionResult<GameViewModel>> NewGame([FromBody] NewGameViewModel startGame)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var gameStarted = await _gameServices.StartNewGame(startGame);
            return Ok(gameStarted);
        }

        /// <summary>
        /// Gets a list of games with player names that have not yet been completed
        /// </summary>
        /// <returns>List of active games</returns>
        /// <response code="200">
        /// A list of active games. Or if there is none,
        /// still returns a response message letting the user know there is no active games
        /// </response>
        [HttpGet("activegames")]
        public async Task<ActionResult<List<ActiveGameViewModel>>> CurrentlyActiveGames()
        {
            var result = await _gameServices.GetActiveGames();
            if (result == null)
                return Ok("No active games");

            return Ok(result);
        }

    }
}
