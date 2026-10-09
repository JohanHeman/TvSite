using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain;
using TvSite.Domain.Entities.Database;

namespace TvSite.Tests.UnitTests.DomainTests
{
    public class ApplicationUserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void Set_DisplayName_ThrowsArgumentException_WhenIsNullOrWhiteSpace(string? displayName)
        {
            // Arrange
            ApplicationUser user = new ApplicationUser();
            var expectedMessage = $"DisplayName cannot be null or whitespace";
            // Act
            var actual = Assert.Throws<ArgumentException>(() => user.DisplayName = displayName!);
            // Assert
            Assert.Equal(expectedMessage, actual.Message);

        }

        [Theory]
        [InlineData("g")]
        [InlineData("Ge")]
        [InlineData(" s")]
        public void Set_DisplayName_ThrowsArgumentException_WhenLengthIsBelowThree(string displayName)
        {
            // Arrange
            ApplicationUser user = new ApplicationUser();
            var expectedMessage = $"DisplayName length cannot be below {ApplicationSettings.DisplayNameMinLength}";
            // Act
            var actual = Assert.Throws<ArgumentException>(() => user.DisplayName = displayName);
            // Assert
            Assert.Equal(expectedMessage, actual.Message);

        }

        [Theory]
        [InlineData("asdasdasdasdsadasadasdas")]
        [InlineData("asdfgasdfgasdfggg")]
        public void Set_DisplayName_ThrowsArgumentException_WhenLengthIsAboveSixteen(string displayName)
        {
            // Arrange
            ApplicationUser user = new ApplicationUser();
            var expectedMessage = $"DisplayName length cannot be longer than {ApplicationSettings.DisplayNameMaxLength}";
            // Act
            var actual = Assert.Throws<ArgumentException>(() => user.DisplayName = displayName);
            // Assert
            Assert.Equal(expectedMessage, actual.Message);

        }

        [Theory]
        [InlineData("alexandra")]
        [InlineData("aLexandra")]
        [InlineData("aLEXANDRA")]
        public void Set_DisplayName_ThrowsArgumentException_WhenFirstLetterIsNotUpperCase(string displayName)
        {
            // Arrange
            ApplicationUser user = new ApplicationUser();
            var expectedMessage = $"First letter must be uppercase";

            // Act
            var actual = Assert.Throws<ArgumentException>(() => user.DisplayName = displayName);

            // Assert
            Assert.Equal(expectedMessage, actual.Message);
        }
    }
}
