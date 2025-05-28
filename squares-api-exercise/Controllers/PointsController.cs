using Microsoft.AspNetCore.Mvc;
using squares_api_excercise.DTOs;
using squares_api_excercise.Services.Interfaces;
using Swashbuckle.AspNetCore.Annotations;

namespace squares_api_excercise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {
        private readonly IPointsService _pointsService;
        private readonly ISquaresService _squaresService;

        public PointsController(IPointsService pointsService, ISquaresService squaresService)
        {
            _pointsService = pointsService;
            _squaresService = squaresService;
        }

        /// <summary>
        /// Gets a list of all points in database.
        /// </summary>
        /// 
        /// <response code="200">The point list was successfully retrieved.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointDTO>>> GetPoints()
        {
            return Ok(await _pointsService.GetPointsAsync());
        }

        // GET: api/Points/5
        /// <summary>
        /// Retrieves point coordinates by id
        /// </summary>
        /// <param name="id">unique id of point</param>
        /// <response code="200">The point was successfully retrieved.</response>
        /// <response code="404">The point with the given ID was not found.</response>
        [HttpGet("{id}")]
        [SwaggerOperation(Description = "Retrieves one point with specified id")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PointDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PointDTO>> GetPoint(int id)
        {
            var point = await _pointsService.GetByIdAsync(id);
            if (point == null)
            {
                return NotFound();
            }
            return point;
        }

        /// <summary>
        /// Allows to insert many points to db.
        /// </summary>
        /// <remarks>
        /// Example request:
        /// ```json
        /// { "points": [ {"x": 1, "y": 2}, {"x": 3, "y": 4}]}
        /// ```
        /// </remarks>
        /// <param name="request">a list of points, in json format, to insert</param>
        /// <response code="200">The points were successfully inserted to db.</response>
        /// <response code="400">the list of points was not provided.</response>
        [SwaggerOperation(Description = "inserts a list of points to db")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("list")]
        public async Task<IActionResult> ImportPoints([FromBody] ImportPointsRequest request)
        {
            if (request?.Points == null || request.Points.Count == 0)
            {
                return BadRequest("Point list is empty or null.");
            }
            var count = await _pointsService.PostManyPoints(request);

            return Ok(new { Message = $"{count} points imported." });
        }

        /// <summary>
        /// Detects all squares that are not already in database.
        /// </summary>
        /// <remarks>
        ///  Example request:
        /// ```json
        /// { "points": [ {"x": 1, "y": 2}, {"x": 3, "y": 4}]}
        /// ```
        /// </remarks>
        /// <response code="200">All new squares have been found and inserted.</response> 
        [HttpGet("squares/detect")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        public async Task<ActionResult<int>> GetSquareCount()
        {
            int count = await _squaresService.FindSquares();
            return Ok(new { Message = $"{count} NEW squares found." });
        }

        // POST: api/Points
        /// <summary>
        /// Inserts a point into database with provided coordinates.
        /// </summary>
        /// <param name="point"></param>
        ///  /// <remarks>
        ///  Example request:
        /// ```json
        /// {"x": 1, "y": 2}
        /// ```
        /// </remarks>
        /// <response code="201">The point was successfully inserted to db.</response>
        /// <response code="400">The point was not inserted to db.</response>
        [HttpPost]
        [SwaggerOperation(Description = "Inserts a point into database with provided coordinates.")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PointDTO>> PostPoint(PointDTO point)
        {
            var result = await _pointsService.AddPoint(point);
            if (result != null)
                return CreatedAtAction(nameof(GetPoint), new { id = result.Id }, result);

            return BadRequest();
        }

        // DELETE: api/Points/5
        /// <summary>
        /// deletes a point with specified id from database
        /// </summary>
        /// <param name="id">id of point to delete</param>
        /// <response code="200">The point were successfully deleted from db.</response>
        /// <response code="400">The point was not deleted from db.</response>
        [HttpDelete("{id}")]
        [SwaggerOperation(Description = "deletes a point with specified id from database.")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IActionResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePoint(int id)
        {
            var result = await _pointsService.DeletePoint(id);
            if (result)
                return Ok();
            return BadRequest();
        }
    }
}
