using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using TvSite.Domain;
using TvSite.Domain.Entities;

namespace TvSite.Tests.UnitTests.DomainTests
{
    public class DomainTests
    {
        // ApplicationUser
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Set_DisplayName_ThrowsArgumentException_WhenIsNullOrWhiteSpace(string? input)
        {
            // Arrange
            ApplicationUser user = new ApplicationUser();
            var expectedMessage = $"DisplayName cannot be null or whitespace";
            // Act
            var actual = Assert.Throws<ArgumentException>(() => user.DisplayName = input);
            // Assert
            Assert.Equal(actual.Message, expectedMessage);

        }

        [Theory]
        [InlineData("g")]
        [InlineData("Ge")]
        [InlineData(" s")]
        public void Set_DisplayName_ThrowsArgumentException_WhenLengthIsBelowThree(string input)
        {
            // Arrange
            ApplicationUser user = new ApplicationUser();
            var expectedMessage = $"DisplayName length cannot be below {ApplicationSettings.DisplayNameMinLength}";
            // Act
            var actual = Assert.Throws<ArgumentException>(() => user.DisplayName = input);
            // Assert
            Assert.Equal(actual.Message, expectedMessage);

        }

        [Theory]
        [InlineData("asdasdasdasdsadasadasdas")]
        [InlineData("asdfgasdfgasdfggg")]
        public void Set_DisplayName_ThrowsArgumentException_WhenLengthIsAboveSixteen(string input)
        {
            // Arrange
            ApplicationUser user = new ApplicationUser();
            var expectedMessage = $"DisplayName length cannot be longer than {ApplicationSettings.DisplayNameMaxLength}";
            // Act
            var actual = Assert.Throws<ArgumentException>(() => user.DisplayName = input);
            // Assert
            Assert.Equal(actual.Message, expectedMessage);

        }

        [Theory]
        [InlineData("alexandra")]
        [InlineData("aLexandra")]
        [InlineData("aLEXANDRA")]
        public void Set_DisplayName_ThrowsArgumentException_WhenFirstLetterIsNotUpperCase(string input)
        {
            // Arrange
            ApplicationUser user = new ApplicationUser();
            var expectedMessage = $"First letter must be uppercase";

            // Act
            var actual = Assert.Throws<ArgumentException>(() => user.DisplayName = input);

            // Assert
            Assert.Equal(actual.Message, expectedMessage);
        }

        // Comment
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CommentText_ThrowsArgumentException_WhenIsNullOrWhiteSpace(string? input)
        {
            // Arrange
            Comment comment = new Comment();
            var expectedMessage = $"Text cannot be null or whitespace";

            // Act
            var actual = Assert.Throws<ArgumentException>(() => comment.Text = input);

            // Assert
            Assert.Equal(actual.Message, expectedMessage);
        }

        // Rating
        [Theory]
        [InlineData(-1)]
        public void Stars_ThrowsArgumentException_WhenBelowMinValue(int stars)
        {
            // Arrange
            Rating rating = new Rating();
            var expectedMessage = $"Stars cannot be below {ApplicationSettings.StarsMin}";

            // Act
            var actual = Assert.Throws<ArgumentException>(() => rating.Stars = stars);

            // Assert
            Assert.Equal(actual.Message, expectedMessage);
        }

        [Theory]
        [InlineData(11)]
        public void Stars_ThrowsArgumentException_WhenAboveMinValue(int stars)
        {
            // Arrange
            Rating rating = new Rating();
            var expectedMessage = $"Stars cannot be above {ApplicationSettings.StarsMax}";

            // Act
            var actual = Assert.Throws<ArgumentException>(() => rating.Stars = stars);

            // Assert
            Assert.Equal(actual.Message, expectedMessage);
        }

        // MediaListEntry
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
