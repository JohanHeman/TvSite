using Moq;
using TvSite.Application.Interfaces.Services;
using TvSite.Application.Service;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;

namespace TvSite.Tests.UnitTests.ServiceTests
{
    public class CommentServiceTests
    {
        private readonly ICommentService _commentService;
        private readonly Mock<ICommentRepository> _mockRepo;

        public CommentServiceTests()
        {
            _mockRepo = new Mock<ICommentRepository>();
            _commentService = new CommentService(_mockRepo.Object);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task GetCommentsByMediaIdAsync_ThrowsArgumentExeption_WhenIsNullOrWhiteSpace(string? episodeMediaId)
        {
            // Arrange
            var expectedMessage = $"EpisodeMediaId cannot be null or whitespace";

            // Act
            var actual = await Assert.ThrowsAsync<ArgumentException>(() => _commentService.GetCommentsByEpisodeMediaIdAsync(episodeMediaId!));

            // Assert
            Assert.Equal(expectedMessage, actual.Message);
            _mockRepo.Verify(repo => repo.GetCommentsByMediaIdAsync(episodeMediaId!), Times.Never);
        }

        [Fact]
        public async Task GetCommentByIdAsync_ThrowsArgumentExeption_WhenGuidIsEmpty()
        {
            // Arrange
            var expectedMessage = $"CommentId cannot be empty";

            // Act
            var actual = await Assert.ThrowsAsync<ArgumentException>(() => _commentService.GetCommentByIdAsync(Guid.Empty));

            // Assert
            Assert.Equal(expectedMessage, actual.Message);
            _mockRepo.Verify(repo => repo.GetCommentByIdAsync(Guid.Empty), Times.Never);
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task CreateCommentAsync_ThrowsArgumentExeption_WhenIdIsNullOrWhiteSpace(string? episodeMediaId)
        {
            // Arrange
            var expectedMessage = $"EpisodeMediaId cannot be null or whitespace";

            // Act
            var actual = await Assert.ThrowsAsync<ArgumentException>(() => _commentService.CreateCommentAsync("Comment text", episodeMediaId!, Guid.NewGuid(), DateTime.Now));

            // Assert
            Assert.Equal(expectedMessage, actual.Message);
            _mockRepo.Verify(repo => repo.CreateCommentAsync(It.IsAny<Comment>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCommentAsync_ThrowsArgumentException_WhenCommentIdIsEmpty()
        {
            // Arrange
            var expectedMessage = $"Comment id cannot be empty";

            // Act
            var actual = await Assert.ThrowsAsync<ArgumentException>(() => _commentService.DeleteCommentAsync(Guid.Empty));

            // Assert
            Assert.Equal(expectedMessage, actual.Message);
            _mockRepo.Verify(repo => repo.GetCommentByIdAsync(Guid.Empty), Times.Never());
            _mockRepo.Verify(repo => repo.DeleteCommentAsync(It.IsAny<Comment>()), Times.Never);
        }

        [Fact]
        public async Task DeleteCommentAsync_ThrowsNullReferenceException_WhenCommentIsNull()
        {
            // Arrange
            var expectedMessage = $"Comment does not exist";

            // Act
            var actual = await Assert.ThrowsAsync<NullReferenceException>(() => _commentService.DeleteCommentAsync(Guid.NewGuid()));

            // Assert
            Assert.Equal(expectedMessage, actual.Message);
        }

        [Fact]
        public async Task GetCommentsByEpisodeMediaIdAsync_ShouldReturnCommentsOnEpisode_WhenValidEpisodeMediaId()
        {
            // Arrange
            var episodeMediaId = "24";

            var comments = new List<Comment>()
            {
                // Only setting up needed properties for this test
                new Comment
                {
                    EpisodeMediaId = episodeMediaId
                },
                new Comment
                {
                    EpisodeMediaId = episodeMediaId
                },
                new Comment
                {
                    EpisodeMediaId = episodeMediaId
                }
            };

            _mockRepo.Setup(repo => repo.GetCommentsByMediaIdAsync(episodeMediaId)).ReturnsAsync(comments);

            // Act
            var actual = await _commentService.GetCommentsByEpisodeMediaIdAsync(episodeMediaId);

            // Assert
            _mockRepo.Verify(repo => repo.GetCommentsByMediaIdAsync(episodeMediaId), Times.Once());

            // Checking that the list returns correct amount
            Assert.Equal(3, actual.Count);

            // Checking the ids are correct
            Assert.Equal("24", actual[0].EpisodeMediaId);
            Assert.Equal("24", actual[1].EpisodeMediaId);
            Assert.Equal("24", actual[2].EpisodeMediaId);
        }

        [Fact]
        public async Task GetCommentByIdAsync_ShouldReturnComment_WhenValidCommentId()
        {
            // Arrange
            var comment = new Comment()
            {
                Id = Guid.NewGuid()
            };

            _mockRepo.Setup(repo => repo.GetCommentByIdAsync(comment.Id)).ReturnsAsync(comment);

            // Act
            var actual = await _commentService.GetCommentByIdAsync(comment.Id);

            // Assert
            _mockRepo.Verify(repo => repo.GetCommentByIdAsync(comment.Id), Times.Once());

            Assert.NotNull(actual);
            Assert.Equal(comment.Id, actual.Id);
        }

        [Fact]
        public async Task CreateCommentAsync_CallsRepoOnce_WhenValidProperties()
        {
            // Arrange
            string commentText = "Very good episode";
            string episodeMediaId = "24";
            Guid userId = Guid.NewGuid();
            DateTime dateTime = DateTime.Now;

            // Act
            await _commentService.CreateCommentAsync(commentText, episodeMediaId, userId, dateTime);

            // Assert
            _mockRepo.Verify(repo => repo.CreateCommentAsync(It.IsAny<Comment>()), Times.Once());
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        [InlineData(null)]
        public async Task CreateCommentAsync_DoesNotCallRepo_WhenCommentTextIsNullOrWhiteSpace(string? commentText)
        {
            // Arrange
            string episodeMediaId = "24";
            Guid userId = Guid.NewGuid();
            DateTime dateTime = DateTime.Now;

            // Act
            await _commentService.CreateCommentAsync(commentText!, episodeMediaId, userId, dateTime);

            // Assert
            _mockRepo.Verify(repo => repo.CreateCommentAsync(It.IsAny<Comment>()), Times.Never());
        }

        [Fact]
        public async Task CreateCommentAsync_DoesNotCallRepo_WhenUserIdIsEmpty()
        {
            // Arrange
            Guid userId = Guid.Empty;

            string episodeMediaId = "24";
            string commentText = "Very bad episode";
            DateTime dateTime = DateTime.Now;

            // Act
            await _commentService.CreateCommentAsync(commentText, episodeMediaId, userId, dateTime);

            // Assert
            _mockRepo.Verify(repo => repo.CreateCommentAsync(It.IsAny<Comment>()), Times.Never());
        }

        [Fact]
        public async Task DeleteCommentAsync_CallsRepoOnce_WhenValidCommentId()
        {
            // Arrange
            var comment = new Comment()
            {
                Id = Guid.NewGuid()
            };

            _mockRepo.Setup(repo => repo.GetCommentByIdAsync(comment.Id)).ReturnsAsync(comment);

            // Act
            await _commentService.DeleteCommentAsync(comment.Id);

            // Assert
            _mockRepo.Verify(repo => repo.GetCommentByIdAsync(comment.Id), Times.Once());
            _mockRepo.Verify(repo => repo.DeleteCommentAsync(It.IsAny<Comment>()), Times.Once());
        }
    }
}