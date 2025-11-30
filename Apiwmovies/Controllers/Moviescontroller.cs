using API.P.Movies.DAL.Models.Dtos;
using API.P.Movies.Services.IServices;
using Microsoft.AspNetCore.Mvc;

namespace API.P.Movies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet(Name = "GetMoviesAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ICollection<MovieDto>>> GetMoviesAsync()
        {
            var movies = await _movieService.GetMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id:int}", Name = "GetMovieAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // Informo al cliente que la categoria no fue encontrada

        public async Task<ActionResult<MovieDto>> GetMovieAsync(int id)
        {
            try
            {
                var movieDto = await _movieService.GetMovieAsync(id);
                return Ok(movieDto);
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No se encontró"))
            {
                return NotFound(new { ex.Message });
            }
        }

        [HttpPost(Name = "CreateMovieAsync")]
        //http status codes
        [ProducesResponseType(StatusCodes.Status200OK)] //indica que el resultado esperado es un 200 OK
        [ProducesResponseType(StatusCodes.Status201Created)] //indica que el recurso fue creado exitosamente
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)] // Indica que hubo un conflicto a la hora de guardar el objeto en la base de datos
        public async Task<ActionResult<MovieDto>> CreateMovieAsync([FromBody] MovieCreateUpdateDto movieCreateUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdMovie = await _movieService.CreateMovieAsync(movieCreateUpdateDto);
                //retornamos un 201
                return CreatedAtRoute(
                    "GetMovieAsync",
                    new { id = createdMovie.Id },
                    createdMovie);
            }

            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(new { ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateMovieAsync")]
        //http status codes
        [ProducesResponseType(StatusCodes.Status200OK)] //indica que el resultado esperado es un 200 OK
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)] // Indica que hubo un conflicto a la hora de guardar el objeto en la base de datos
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<MovieDto>> UpdateMovieAsync([FromBody] MovieCreateUpdateDto dto, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedMovie = await _movieService.UpdateMovieAsync(dto, id);
                return Ok(updatedMovie);
            }

            catch (InvalidOperationException ex) when (ex.Message.Contains("Ya existe"))
            {
                return Conflict(new { ex.Message });
            }
            catch (InvalidOperationException ex) when (ex.Message.Contains("No se encontró"))
            {
                return NotFound(new { ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "DeleteMovieAsync")]
        //http status codes
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult> DeleteMovieAsync(int id)
        {
            try
            {
                var deletedMovie = await _movieService.DeleteMovieAsync(id);
                return Ok(deletedMovie); // retorno ok para mostrar el "True"
            }

            catch (InvalidOperationException ex) when (ex.Message.Contains("No se encontró"))
            {
                return NotFound(new { ex.Message });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}

