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

        public MovieRepository()
        {
            var file = File.ReadAllText("Movies.json");
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
    }
}