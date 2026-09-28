using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain;
using TvSite.Domain.Entities;

namespace TvSite.Tests.UnitTests.DomainTests
{
    public class DomainTests
    {

        ApplicationUser _user = new ApplicationUser() {DisplayName = "Johan"};

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Set_DisplayName_ThrowsArgumentException_WhenIsNullOrEmpty(string input)
        {
            // Arrange
            var expectedMessage = $"DisplayName cannot be null or empty";
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


    }
}
