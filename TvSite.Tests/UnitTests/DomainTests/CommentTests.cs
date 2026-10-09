using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain.Entities.Database;

namespace TvSite.Tests.UnitTests.DomainTests
{
    public class CommentTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CommentText_ThrowsArgumentException_WhenIsNullOrWhiteSpace(string? commentText)
        {
            // Arrange
            Comment comment = new Comment();
            var expectedMessage = $"Text cannot be null or whitespace";

            // Act
            var actual = Assert.Throws<ArgumentException>(() => comment.Text = commentText!);

            // Assert
            Assert.Equal(expectedMessage, actual.Message);
        }
    }
}
