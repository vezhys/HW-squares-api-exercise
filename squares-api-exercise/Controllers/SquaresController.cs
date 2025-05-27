using Microsoft.AspNetCore.Mvc;
using squares_api_excercise.DTOs;
using squares_api_excercise.Services;

namespace squares_api_excercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SquaresController : ControllerBase
    {
        private readonly ISquaresService _squaresService;

        public SquaresController(ISquaresService squaresService)
        {
            _squaresService = squaresService;
        }
        /// <summary>
        /// Returns a list of squares
        /// </summary>
        /// <response code="200">The squares were successfully retrieved from db.</response>
        /// <response code="400">the list squares was not retrieved from db</response>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SquareDTO>>> GetSquares()
        {
            try
            {
                var squares = await _squaresService.GetSquaresAsync();
                return Ok(squares);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
