using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using MovieMaster.Data.Dto;
using MovieMaster.Data.Interfaces;
using MovieMaster.Service.Model;
using NUnit.Framework;

namespace MovieMaster.Service.Tests
{
    public class MovieServiceTests
    {
        private Mock<IMovieRepository> _movieRepository;
        private MovieService _fixture;
    
        [SetUp]
        public void Setup()
        {
            var movies = new List<MovieDto>
            {
                new MovieDto { Id = "1", Title = "Encanto" },
                new MovieDto { Id = "2", Title = "Inception" },
            };
            _movieRepository = new Mock<IMovieRepository>();
            _movieRepository.Setup(r => r.GetAllMoviesAsync()).ReturnsAsync(movies);
            _movieRepository.Setup(r => r.GetMovieByIdAsync("1")).ReturnsAsync(movies[0]);

            var mapper = new Mapper(new MapperConfiguration(c =>
            {
                c.AddProfile<AutoMapperProfile>();
            }));
            
            _fixture = new MovieService(mapper, _movieRepository.Object);
        }

        [Test]
        public async Task FindAllAsync_ReturnsAllMovies()
        {
            var movies = await _fixture.FindAllAsync();
            Assert.AreEqual(2, movies.Count());
        }

        [Test]
        public async Task FindMovieById_WhenIdIsInvalid_ReturnNull()
        {
            var movie = await _fixture.FindMovieByIdAsync("INVALID_ID");
            Assert.IsNull(movie);
        }
        
        [Test]
        public async Task FindMovieById_WhenIdIsValid_ReturnMovie()
        {
            var movie = await _fixture.FindMovieByIdAsync("1");
            Assert.AreEqual("Encanto", movie.Title);
        }
        
        [Test]
        public async Task UpdateMovieAsync_WhenIdIsInvalid_ReturnNotFound()
        {
            var result = await _fixture.UpdateMovieAsync("INVALID_ID", new Movie());
            Assert.AreEqual(UpdateMovieStatus.NotFound, result.Status);
        }
        
        [Test]
        public async Task UpdateMovieAsync_WhenRepositoryReturnsNull_ReturnError()
        {
            _movieRepository
                .Setup(r => r.UpdateMovieAsync("1", It.IsAny<MovieDto>()))
                .Returns(() => Task.FromResult<MovieDto>(null));
            
            var result = await _fixture.UpdateMovieAsync("1", new Movie());
            Assert.AreEqual(UpdateMovieStatus.Error, result.Status);
        }
        
        [Test]
        public async Task UpdateMovieAsync_WhenRepositoryReturnsMovie_ReturnSuccess()
        {
            _movieRepository
                .Setup(r => r.UpdateMovieAsync("1", It.IsAny<MovieDto>()))
                .ReturnsAsync(new MovieDto());
            
            var result = await _fixture.UpdateMovieAsync("1", new Movie());
            Assert.AreEqual(UpdateMovieStatus.Updated, result.Status);
        }
    }
}