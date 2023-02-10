using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MovieMaster.Data;
using MovieMaster.Data.Dto;
using MovieMaster.Data.Interfaces;
using MovieMaster.Service.Interfaces;
using MovieMaster.Service.Model;

namespace MovieMaster.Service
{
    /// <inheritdoc />
    public sealed class MovieService : IMovieService
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMapper mapper, IMovieRepository movieRepository)
        {
            _mapper = mapper;
            _movieRepository = movieRepository;
        }
        
        public async Task<IEnumerable<Movie>> FindAllAsync()
        {
            var movies = await _movieRepository.GetAllMoviesAsync();

            return movies.Select(m => _mapper.Map<Movie>(m));
        }

        public async Task<Movie> FindMovieByIdAsync(string id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            return movie == null 
                ? null 
                : _mapper.Map<Movie>(movie);
        }

        public async Task<UpdateMovieResult> UpdateMovieAsync(string id, Movie movie)
        {
            // Check the movie exists
            var movieInDb = await _movieRepository.GetMovieByIdAsync(id);
            if (movieInDb == null)
            {
                return new UpdateMovieResult { Status = UpdateMovieStatus.NotFound };
            }
            
            var movieDto = _mapper.Map<MovieDto>(movie);
            
            // Update the movie
            var result = await _movieRepository.UpdateMovieAsync(id, movieDto);
            if (result == null)
            {
                return new UpdateMovieResult { Status = UpdateMovieStatus.Error };
            }

            // Return success
            return new UpdateMovieResult
            {
                Status = UpdateMovieStatus.Updated,
                Movie = _mapper.Map<Movie>(result)
            };
        }
    }
}