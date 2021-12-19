using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.App.Services.Interfaces;
using TicTacToe.Models.ViewModels;

namespace TicTacToe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerServices _playerServices;

        public PlayerController(IPlayerServices playerServices)
        {
            _playerServices = playerServices;
        }

        /// <summary>
        /// With player's Id, places the move on the game board
        /// </summary>
        /// <param name="playerMove"></param>
        /// <returns>Returns the current state of the game board along with playerId and message that clarifies the status of the game</returns>
        /// <remarks>
        ///     Sample Request:
        ///     PUT
        ///     {
        ///             "playerid": "a60a65fb-2edf-41c8-b83f-29d85c7f8d8e",
        ///             "target": {"row":2, "col":0}
        ///     }
        /// </remarks>
        /// <response code="200">{ gameboard:[--state of the board--], playerId: Guid, message: string } </response>
        [HttpPut("placemove")]
        public async Task<ActionResult<GameBoardViewModel>> MoveInput(MoveInputViewModel playerMove)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _playerServices.PlaceMove(playerMove);
            return Ok(result);
        }
    }
}
