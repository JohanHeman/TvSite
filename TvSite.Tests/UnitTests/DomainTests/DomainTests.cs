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
        ApplicationUser _user = new ApplicationUser() {DisplayName = "Johan"};
        Comment _comment = new Comment();
        Rating _rating = new Rating();
        MediaListEntry _mediaListEntry = new MediaListEntry();

        // ApplicationUser
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Set_DisplayName_ThrowsArgumentException_WhenIsNullOrWhiteSpace(string input)
        {
            // Arrange
            var expectedMessage = $"DisplayName cannot be null or whitespace";
            // Act
            var actual = Assert.Throws<ArgumentException>(() => _user.DisplayName = input);
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
            var expectedMessage = $"DisplayName length cannot be below {ApplicationSettings.DisplayNameMinLength}";
            // Act
            var actual = Assert.Throws<ArgumentException>(() => _user.DisplayName = input);
            // Assert
            Assert.Equal(actual.Message, expectedMessage);

        }

        [Theory]
        [InlineData("asdasdasdasdsadasadasdas")]
        [InlineData("asdfgasdfgasdfggg")]
        public void Set_DisplayName_ThrowsArgumentException_WhenLengthIsAboveSixteen(string input)
        {
            // Arrange
            var expectedMessage = $"DisplayName length cannot be longer than {ApplicationSettings.DisplayNameMaxLength}";
            // Act
            var actual = Assert.Throws<ArgumentException>(() => _user.DisplayName = input);
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
            var expectedMessage = $"First letter must be uppercase";

            // Act
            var actual = Assert.Throws<ArgumentException>(() => _user.DisplayName = input);

            // Assert
            Assert.Equal(actual.Message, expectedMessage);
        }

        // Comment
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void CommentText_ThrowsArgumentException_WhenIsNullOrWhiteSpace(string input)
        {
            // Arrange
            var expectedMessage = $"Text cannot be null or whitespace";
            
            // Act
            var actual = Assert.Throws<ArgumentException>(() => _comment.Text = input);
            
            // Assert
            Assert.Equal(actual.Message, expectedMessage);
        }

        // Rating
        [Theory]
        [InlineData(-1)]
        public void Stars_ThrowsArgumentException_WhenBelowMinValue(int stars)
        {
            // Arrange
            var expectedMessage = $"Stars cannot be below {ApplicationSettings.StarsMin}";
            
            // Act
            var actual = Assert.Throws<ArgumentException>(() => _rating.Stars = stars);
            
            // Assert
            Assert.Equal(actual.Message, expectedMessage);
        }

        [Theory]
        [InlineData(11)]
        public void Stars_ThrowsArgumentException_WhenAboveMinValue(int stars)
        {
            // Arrange
            var expectedMessage = $"Stars cannot be above {ApplicationSettings.StarsMax}";

            // Act
            var actual = Assert.Throws<ArgumentException>(() => _rating.Stars = stars);

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
            var expectedMessage = $"List state does not exist";

            // Act
            var actual = Assert.Throws<ArgumentException>(() => _mediaListEntry.ListState = input);

            // Assert
            Assert.Equal(actual.Message , expectedMessage);
        }
    }
}
