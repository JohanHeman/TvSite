using System;
using System.Collections.Generic;
using System.Text;
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
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>( () => _user.DisplayName = input);

        }

        [Theory]
        [InlineData("g")]
        [InlineData("Ge")]
        [InlineData(" s")]
        public void Set_DisplayName_ThrowsArgumentException_WhenLengthIsBelowThree(string input)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => _user.DisplayName = input);

        }

        [Theory]
        [InlineData("asdasdasdasdsadasadasdas")]
        [InlineData("asdfgasdfgasdfggg")]
        public void Set_DisplayName_ThrowsArgumentException_WhenLengthIsAboveSixteen(string input)
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => _user.DisplayName = input);

        }


    }
}
