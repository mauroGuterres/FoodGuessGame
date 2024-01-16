using GuessFoodGame.Models;
using GuessFoodGame.Service;
using GuessFoodGame.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

namespace YourApp.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class FoodGuessingGameController : ControllerBase
    {
        private readonly IFoodGuessingGameService gameService;

        public FoodGuessingGameController(IFoodGuessingGameService gameService)
        {
            this.gameService = gameService;
        }

        /// <summary>
        /// Starts a new game session.
        /// </summary>
        /// <remarks>
        /// Call this method to start a new game session. It returns a session ID which you will need to use for subsequent requests to the answer endpoint.
        /// </remarks>
        /// <returns>A session ID for starting a new game session along with a message prompting the user to think of a dish they like.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("startGame")]
        public IActionResult StartGame()
        {
            var sessionId = gameService.StartNewGame();
            return Ok(new
            {
                SessionId = sessionId,
                Message = "Por favor, pense em um prato que gosta."
            });
        }

        /// <summary>
        /// Processes the user's answer and progresses the game.
        /// </summary>
        /// <remarks>
        /// This endpoint processes the user's answer to the current question or guess.
        /// Based on the answer, it will either proceed to the next question, make a guess,
        /// or enter into the learning phase if the guess was incorrect.
        /// </remarks>
        /// <param name="resposta">The user's answer, including the session ID from the startGame endpoint.</param>
        /// <returns>A message indicating the next question, a guess, or a prompt for learning a new food.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("answer")]
        public IActionResult AnswerQuestion([FromBody]UserResponse resposta)
        {
            var response = gameService.ProcessAnswer(resposta.sessionId, resposta.answer);
            return Ok(response);
        }
    }




}

