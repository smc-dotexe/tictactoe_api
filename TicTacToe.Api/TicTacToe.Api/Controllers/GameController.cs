using Microsoft.AspNetCore.Mvc;
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
        private readonly IPlayerServices _playerServices;

        public GameController(IGameServices gameServices, IPlayerServices playerServices)
        {
            _gameServices = gameServices;
            _playerServices = playerServices;
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

        [HttpPost("move")]
        public async Task<ActionResult<GameBoardViewModel>> MoveInput(MoveInputViewModel playerMove)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _playerServices.PlaceMove(playerMove);
            return Ok(result);
        }
    }
}
