using FluentAssertions;
using Moq;
using squares_api_excercise.Models;
using squares_api_excercise.Repositories;
using squares_api_excercise.Services;

namespace SquaresApi.Tests
{
    public class SquaresServiceTests
    {
        private readonly Mock<ISquaresRepository> _mockSquareRepo;
        private readonly Mock<IPointsRepository> _mockPointsRepo;
        private readonly ISquaresService _service;
        public SquaresServiceTests()
        {
            _mockSquareRepo = new Mock<ISquaresRepository>();
            _mockPointsRepo = new Mock<IPointsRepository>();
            _service = new SquaresService(_mockSquareRepo.Object, _mockPointsRepo.Object);
        }

        [Fact]
        public async Task GetSquaresAsync_ReturnsExpectedDTOs()
        {
            // Arrange: create some sample Squares with Points
            var squares = new List<Square>
            {
                new Square
                {
                    P1 = new Point { X = 1, Y = 1 },
                    P2 = new Point { X = 2, Y = 1 },
                    P3 = new Point { X = 2, Y = 2 },
                    P4 = new Point { X = 1, Y = 2 }
                }
            };

            _mockSquareRepo
                .Setup(repo => repo.GetSquaresAsync())
                .ReturnsAsync(squares);

            var result = await _service.GetSquaresAsync();


            result.Should().NotBeNull();
            result.Should().HaveCount(1);

            var dto = result[0];
            dto.P1.X.Should().Be(1);
            dto.P1.Y.Should().Be(1);
            dto.P2.X.Should().Be(2);
            dto.P2.Y.Should().Be(1);
            dto.P3.X.Should().Be(2);
            dto.P3.Y.Should().Be(2);
            dto.P4.X.Should().Be(1);
            dto.P4.Y.Should().Be(2);
        }

        [Fact]
        public async Task GetSquaresAsync_ReturnsEmpty()
        {
            var squares = new List<Square>();

            _mockSquareRepo
                .Setup(repo => repo.GetSquaresAsync())
                .ReturnsAsync(squares);

            var result = await _service.GetSquaresAsync();

            result.Should().HaveCount(0); 
        }

        [Theory]
        [InlineData(new int[] { 1, 2, 3, 4 }, true)]
        [InlineData(new int[] { 5, 6, 7, 8 }, false)]
        [InlineData(new int[] { 2, 1, 4, 3 }, true)]
        public async Task SquareExists_ReturnsExpected(int[] pointIds, bool expected)
        {

            var square = new Square { P1Id = 1, P2Id = 2, P3Id = 3, P4Id = 4};
            _mockSquareRepo
                .Setup(repo => repo.GetSquaresAsync())
                .ReturnsAsync(new List<Square> { square });
            var result = await _service.SquareExistsAsync(pointIds);

            Assert.Equal(expected, result);
        }

        [Fact]
        public async Task FindSquares_ShouldIdentifySquareAndSaveIt()
        {

            var points = new List<Point>
            {
                new Point { Id = 1, X = 0, Y = 0 },
                new Point { Id = 2, X = 1, Y = 0 },
                new Point { Id = 3, X = 1, Y = 1 },
                new Point { Id = 4, X = 0, Y = 1 }
            };

            _mockPointsRepo
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(points);

            _mockSquareRepo
                .Setup(repo => repo.GetSquaresAsync())
                .ReturnsAsync(new List<Square>());

            _mockSquareRepo
                .Setup(repo => repo.AddSquare(It.IsAny<Square>()))
                .Returns(Task.CompletedTask);

            var result = await _service.FindSquares();

            result.Should().Be(1);
            _mockSquareRepo.Verify(repo => repo.AddSquare(It.IsAny<Square>()), Times.Once);
        }

        [Fact]
        public async Task FindSquares_ShouldIdentifyNoSquares()
        {
            var points = new List<Point>
            {
                new Point { Id = 1, X = 0, Y = 0 },
                new Point { Id = 2, X = 1, Y = 2 },
                new Point { Id = 3, X = 1, Y = 1 },
                new Point { Id = 4, X = 0, Y = 1 }
            };

            _mockPointsRepo
                .Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(points);

            _mockSquareRepo
                .Setup(repo => repo.GetSquaresAsync())
                .ReturnsAsync(new List<Square>());

            _mockSquareRepo
                .Setup(repo => repo.AddSquare(It.IsAny<Square>()))
                .Returns(Task.CompletedTask);

            var result = await _service.FindSquares();

            result.Should().Be(0);
            _mockSquareRepo.Verify(repo => repo.AddSquare(It.IsAny<Square>()), Times.Never);
        }
    }
}