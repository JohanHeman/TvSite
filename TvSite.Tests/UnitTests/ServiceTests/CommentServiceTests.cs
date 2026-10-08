using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TvSite.Application.Service;
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
        public async Task GetCommentsByMediaIdAsync_ThrowsArgumentExeption_WhenIsNullOrWhiteSpace(string? input)
        {
            // Arrange
            var expectedMessage = $"MediaId cannot be null or whitespace";

            // Act
            var actual = await Assert.ThrowsAsync<ArgumentException>(() => _commentService.GetCommentsByEpisodeMediaIdAsync(input!));

            // Assert
            Assert.Equal(expectedMessage, actual.Message);
            _mockRepo.Verify(repo => repo.GetCommentsByMediaIdAsync(input!), Times.Never);
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
    }
}
