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
            _logger.LogInformation("Getting all movies"); //#Task5

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
            _logger.LogInformation("Getting movie by ID: {0}", id); //#Task5
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
            _logger.LogInformation("Updating movie with ID: {0}", id);//#Task5
            var result = await _movieService.UpdateMovieAsync(id, movie);
            switch (result.Status)
            {
                case UpdateMovieStatus.Updated:
                    _logger.LogInformation("Movie with ID {0} updated successfully", id);//#Task5
                    return Ok(result.Movie);
                case UpdateMovieStatus.NotFound:
                    _logger.LogWarning("Movie with ID {0} not found", id);//#Task5
                    return NotFound();
                default:
                    _logger.LogError("Unknown error while updating movie with ID: {0}", id);//#Task5
                    return StatusCode(500);
            }
        }

        //#Task2
        /// <summary>
        /// Retrieves a list of movies by year
        /// </summary>
        /// <param name="year">Year of the movies to get.</param>
        /// <returns></returns>
        [HttpGet]
//        [Route("{year}")]
        [Route("year/{year}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Movies", typeof(Movie[]))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Movies does not exist")]
        public async Task<IActionResult> GetByYearAsync(string year)
        {
            _logger.LogInformation("Executing GetByYearAsync");//#Task5
            var movies = await _movieService.FindMoviesByYearAsync(year);
            if (movies == null)
            {
                _logger.LogInformation("No movies found for year {year}", year);//#Task5
                return NotFound();
            }
            _logger.LogInformation("Movies found for year {year}: {movies}", year, movies);//#Task5
            return Ok(movies);
        }


        //#Task3
        /// <summary>
        /// Add a movie.
        /// </summary>
        /// <param name="title">Title of the movie.</param>
        /// <param name="movie">Details of movie.</param>
        /// <returns>The Added Movie.</returns>
        [HttpPost]
        [Route("{title}")]
        [SwaggerResponse(StatusCodes.Status200OK, "Movie", typeof(Movie))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Unknown Error")]
        public async Task<IActionResult> AddMovieAsync(string title, [FromBody] Movie movie)
        {
            _logger.LogInformation("Executing AddMovieAsync");//#Task5
            var result = await _movieService.AddMovieAsync(title, movie);
            switch (result.Status)
            {
                case AddMovieStatus.Added:
                    _logger.LogInformation("Movie added with ID {id}", result.Movie.Id);//#Task5
                    return Ok(result.Movie);
                default:
                    _logger.LogInformation("Failed to add movie with title {title}", title);//#Task5
                    return StatusCode(500);
            }
        }

    }
}