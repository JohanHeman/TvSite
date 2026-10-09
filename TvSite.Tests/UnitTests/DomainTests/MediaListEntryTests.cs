using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain.Entities.Database;

namespace TvSite.Tests.UnitTests.DomainTests
{
    public class MediaListEntryTests
    {
        [Theory]
        [InlineData(4)]
        [InlineData(-1)]
        [InlineData(0)]
        public void ListState_ThrowsArgumentExeption_WhenListStateDoesNotExist(int input)
        {
            // Arrange
            MediaListEntry mediaListEntry = new MediaListEntry();
            var expectedMessage = $"List state does not exist";

            // Act
            var actual = Assert.Throws<ArgumentException>(() => mediaListEntry.ListState = input);

            // Assert
            Assert.Equal(actual.Message, expectedMessage);
        }
    }
}
