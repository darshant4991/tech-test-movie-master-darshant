using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieMaster.Data.Dto;
using NUnit.Framework;

namespace MovieMaster.Data.Tests
{
    public class MovieRepositoryTests
    {
        private readonly List<MovieDto> _movies = new List<MovieDto>
        {
                new MovieDto { Id = "1", Title = "Encanto", Year="2016" },
                new MovieDto { Id = "2", Title = "Inception", Year="2016" },
        };


        private MovieRepository _fixture;

        [SetUp]
        public void Setup()
        {
            _fixture = new MovieRepository(_movies);
        }

        [Test]
        public async Task GetAllMoviesAsync_ReturnsAllMovies()
        {
            var result = await _fixture.GetAllMoviesAsync();
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetMovieByIdAsync_WhenIdIsInvalid_ReturnNull()
        {
            var result = await _fixture.GetMovieByIdAsync("INVALID_ID");
            Assert.IsNull(result);
        }
        
        [Test]
        public async Task GetMovieByIdAsync_WhenIdIsValid_ReturnMovie()
        {
            var result = await _fixture.GetMovieByIdAsync("1");
            Assert.AreEqual("Encanto", result.Title);
        }
        
        [Test]
        public async Task UpdateMovieAsync_WhenIdIsInvalid_ReturnNull()
        {
            var result = await _fixture.UpdateMovieAsync("INVALID_ID", _movies[0]);
            Assert.IsNull(result);
        }
        
        [Test]
        public async Task UpdateMovieAsync_WhenIdIsInvalid_UpdatesTheMovie()
        {
            var result = await _fixture.UpdateMovieAsync("1", new MovieDto
            {
                Title = "New Title",
                Year = "2022"
            });

            Assert.AreEqual("New Title", result.Title);
        }

        //#Test2
        [Test]
        public async Task GetAllMoviesByYearAsync_ReturnsAllMovies()
        {
            var result = await _fixture.GetAllMoviesByYearAsync("2016");
            Assert.AreEqual(2, result.Count());
        }

        //#Test3
        [Test]
        public async Task AddMovieAsync_ReturnsNullWhenMovieDetailsNotProvided()
        {
            // Act
            var result = await _fixture.AddMovieAsync("", null);

            // Assert
            Assert.IsNull(result);
        }

        //#Test3
        [Test]
        public async Task AddMovieAsync_AddsMovieToDictionary()
        {
            // Arrange
            var movie = new MovieDto
            {
                Title = "Test Movie",
                Year = "2022",
                Actors = "John Doe, Jane Smith",
                Rating ="8.0",
                LastUpdated = DateTime.Now
            };

            // Act
            var result = await _fixture.AddMovieAsync(movie.Title, movie);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(movie.Title, result.Title);
            Assert.AreEqual(movie.Year, result.Year);
            Assert.AreEqual(movie.Actors, result.Actors);
            Assert.AreEqual(movie.Rating, result.Rating);
            Assert.AreEqual(movie.LastUpdated, result.LastUpdated);
        }
    }
}