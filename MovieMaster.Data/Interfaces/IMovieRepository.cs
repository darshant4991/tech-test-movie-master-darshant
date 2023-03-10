using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMaster.Data.Dto;

namespace MovieMaster.Data.Interfaces
{
    public interface IMovieRepository
    {
        /// <summary>
        /// Gets all movies.
        /// </summary>
        /// <returns>An enumerable list of movies.</returns>
        Task<IEnumerable<MovieDto>> GetAllMoviesAsync();

        /// <summary>
        /// Gets a movie by the specified ID.
        /// </summary>
        /// <param name="id">ID of the movie.</param>
        /// <returns>A <see cref="MovieDto"/> object.</returns>
        Task<MovieDto> GetMovieByIdAsync(string id);

        /// <summary>
        /// Updates the specific movie.
        /// </summary>
        /// <param name="id">Id of the movie to be updated.</param>
        /// <param name="details">Movie to update.</param>
        /// <returns>The updated movie.</returns>
        Task<MovieDto> UpdateMovieAsync(string id, MovieDto details);

        //#Task2
        /// <summary>
        /// Gets all movies by the specific Year.
        /// </summary>
        /// <param name="year">Year of the movies.</param>
        /// <returns>An enumerable list of movies for given year.</returns>
        Task<IEnumerable<MovieDto>> GetAllMoviesByYearAsync(string year);

        //#Task3
        /// <summary>
        /// Add the movie.
        /// </summary>
        /// <param name="title">Id of the movie to be updated.</param>
        /// <param name="details">Movie to update.</param>
        /// <returns>The Added movie.</returns>
        Task<MovieDto> AddMovieAsync(string title, MovieDto details);
        
    }
}