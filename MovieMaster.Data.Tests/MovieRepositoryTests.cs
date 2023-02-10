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
            new MovieDto { Id = "1", Title = "Encanto" },
            new MovieDto { Id = "2", Title = "Inception" },
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
    }
}