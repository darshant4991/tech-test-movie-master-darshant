using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieMaster.Service;
using MovieMaster.Service.Interfaces;
using MovieMaster.Service.Model;
using Swashbuckle.AspNetCore.Annotations;

namespace MovieMaster.Controllers
{
    /// <summary>
    /// Handles CRUD operations on movies.
    /// </summary>
    [ApiController]
    [Route("movie")]
    [Produces("application/json")]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> _logger;
        private readonly IMovieService _movieService;

        /// <summary>
        /// Creates a new instance of <see cref="MovieController"/>.
        /// </summary>
        /// <param name="logger">An instance of <see cref="ILogger"/>.</param>
        /// <param name="movieService">An instance of <see cref="IMovieService"/>.</param>
        public MovieController(ILogger<MovieController> logger, IMovieService movieService)
        {
            _logger = logger;
            _movieService = movieService;
        }

        /// <summary>
        /// Retrieves a list of movies
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "Movies", typeof(Movie[]))]
        public async Task<IActionResult> GetAsync()
        {
            var movies = await _movieService.FindAllAsync();

            return Ok(movies);
        }

        /// <summary>
        /// Retrieves a specific movie by ID.
        /// </summary>
        /// <param name="id">ID of the movie to get.</param>
        /// <returns>A Movie.</returns>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Movie", typeof(Movie))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Movie does not exist")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var movie = await _movieService.FindMovieByIdAsync(id);
            return movie == null 
                ? (IActionResult)NotFound() 
                : Ok(movie);
        }

        /// <summary>
        /// Update a movie.
        /// </summary>
        /// <param name="id">ID of the movie.</param>
        /// <param name="movie">Details of movie.</param>
        /// <returns>The updated Movie.</returns>
        [HttpPut]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Movie", typeof(Movie))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Movie does not exist")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown Error")]
        public async Task<IActionResult> UpdateMovieAsync(string id, [FromBody]Movie movie)
        {
            var result = await _movieService.UpdateMovieAsync(id, movie);
            switch (result.Status)
            {
                case UpdateMovieStatus.Updated: return Ok(result.Movie);
                case UpdateMovieStatus.NotFound: return NotFound();
                default: return StatusCode(500);
            }
        }
    }
}