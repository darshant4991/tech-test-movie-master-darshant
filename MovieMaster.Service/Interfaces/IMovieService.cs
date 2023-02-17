using System.Collections.Generic;
using System.Threading.Tasks;
using MovieMaster.Service.Model;

namespace MovieMaster.Service.Interfaces
{
    public interface IMovieService
    {
        /// <summary>
        /// Get all movies.
        /// </summary>
        /// <returns>An Enumerable list of Movie objects.</returns>
        Task<IEnumerable<Movie>> FindAllAsync();

        /// <summary>
        /// Gets a movie by Id.
        /// </summary>
        /// <param name="id">Id of the movie.</param>
        /// <returns>A <see cref="Movie"/> object.</returns>
        Task<Movie> FindMovieByIdAsync(string id);

        /// <summary>
        /// Updates the specific movie.
        /// </summary>
        /// <param name="movie">Movie to update.</param>
        /// <returns>The updated movie.</returns>
        Task<UpdateMovieResult> UpdateMovieAsync(string id, Movie movie);

        //#Task2
        /// <summary>
        /// Gets a movies by the specific Year.
        /// </summary>
        /// <param name="year">Year of the movie.</param>
        /// <returns>An enumerable list of movies for given year.</returns>
        Task<IEnumerable<Movie>> FindMoviesByYearAsync(string year);

        //#Task3
        /// <summary>
        /// Add the movie.
        /// </summary>
        /// <param name="movie">Movie to update.</param>
        /// <returns>The Added movie.</returns>
        Task<AddMovieResult> AddMovieAsync(string title, Movie movie);



    }
}