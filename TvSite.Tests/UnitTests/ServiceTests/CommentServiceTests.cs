using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TvSite.Application.Service;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;

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
            var actual = await Assert.ThrowsAsync<ArgumentException>(() => _commentService.CreateCommentAsync("Comment text", episodeMediaId!, Guid.NewGuid()));

            // Assert
            Assert.Equal(expectedMessage, actual.Message);
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
            _mockRepo.Verify(repo => repo.DeleteCommentAsync(null!), Times.Never);
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
    }
}
