using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Application.Helpers;

namespace TvSite.Tests.UnitTests
{
    public class TextFormatingHelpersTests
    {
        [Theory]
        [InlineData("alexandra", "Alexandra")]
        [InlineData("aLEX", "ALEX")]
        [InlineData("tv Johan", "Tv Johan")]
        public void SetFirstLetterToUpper_ShouldReturn_FirstLetterUpperCased_WhenFirstLetterIsLowerCase(string input, string expected)
        {
            // Act
            var actual = TextFormatingHelpers.SetFirstLetterToUpper(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData(" ", " ")]
        [InlineData("  ", "  ")]
        public void SetFirstLetterToUpper_ShouldReturn_InputWhenStringIsNullOrWhiteSpace(string input, string expected)
        {
            // Act
            var actual = TextFormatingHelpers.SetFirstLetterToUpper(input);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
