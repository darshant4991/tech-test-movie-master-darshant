using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using MovieMaster.Data.Dto;
using MovieMaster.Data.Interfaces;
using Newtonsoft.Json;

[assembly: InternalsVisibleTo("MovieMaster.Data.Tests")]

namespace MovieMaster.Data
{
    /// <inheritdoc />
    public sealed class MovieRepository : IMovieRepository
    {
        private readonly Dictionary<string, MovieDto> _movies;
        private readonly string _filePath = "Movies.json"; //#Task3
        public MovieRepository()
        {
            var file = File.ReadAllText(_filePath);// ("Movies.json");//#Task3
            var movieList = JsonConvert.DeserializeObject<List<MovieDto>>(file);
            _movies = movieList.ToDictionary(m => m.Id);
        }

        internal MovieRepository(IEnumerable<MovieDto> movies)
        {
            _movies = movies.ToDictionary(m => m.Id);
        }

        public Task<IEnumerable<MovieDto>> GetAllMoviesAsync()
        {
            return Task.FromResult(_movies.Values.Skip(0));
        }

        public Task<MovieDto> GetMovieByIdAsync(string id)
        {
            return _movies.TryGetValue(id, out var movie) 
                ? Task.FromResult(movie) 
                : Task.FromResult<MovieDto>(null);
        }

        public Task<MovieDto> UpdateMovieAsync(string id, MovieDto details)
        {
            if (_movies.TryGetValue(id, out var movie))
            {
                movie.Title = details.Title;
                movie.Year = details.Year;
                movie.Actors = details.Actors;
                movie.LastUpdated = DateTime.Now;

                return Task.FromResult(movie);
            }
            
            return Task.FromResult<MovieDto>(null);
        }

        //#Task2
        public Task<IEnumerable<MovieDto>> GetAllMoviesByYearAsync(string year)
        {
            return Task.FromResult(_movies.Values.Where(m => m.Year == year));
        }

        //#Task3
        public Task<MovieDto> AddMovieAsync(string title, MovieDto details)
        {

            if (details == null || string.IsNullOrEmpty(details.Title))
            {
                return Task.FromResult<MovieDto>(null);
            }

            // Generate a new ID if it is not provided
            if (string.IsNullOrWhiteSpace(details.Id))
            {
                details.Id = Guid.NewGuid().ToString();
            }

            // Add the movie to the dictionary
            _movies.Add(details.Id, details);

            // Write the updated dictionary back to the JSON file
            string updatedJson = JsonConvert.SerializeObject(_movies, Formatting.Indented);
            System.IO.File.WriteAllText(_filePath, updatedJson);

            return Task.FromResult(details);
        }

    }
}