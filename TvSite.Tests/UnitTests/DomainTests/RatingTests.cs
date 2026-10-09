using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain;
using TvSite.Domain.Entities.Database;

namespace TvSite.Tests.UnitTests.DomainTests
{
    public class RatingTests
    {
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
    }
}
