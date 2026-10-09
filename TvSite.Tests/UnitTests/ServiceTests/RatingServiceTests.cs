using Moq;
using TvSite.Application.Service;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;

namespace TvSite.Tests.UnitTests;

public class RatingServiceTests
{
    private readonly IRatingService _sut;
    private readonly Mock<IRatingRepository> _mockrepo;
    public RatingServiceTests()
    {
        _mockrepo = new Mock<IRatingRepository>();
        _sut = new RatingService(_mockrepo.Object);
    }

    [Theory]
    [InlineData(new int[] { 4, 4, 5, 6, 7 })]
    [InlineData(new int[] { 1, 2, 3, 4, 5 })]
    [InlineData(new int[] { 5, 5, 5, 5, 5 })]
    [InlineData(new int[] { 1, 1, 2, 2, 3 })]
    public async Task GetAverageRatingByMediaIdAsync_ShouldReturnAverageRating(int[] stars)
    {
        var ratings = stars.Select(star => new Rating { Stars = star }).ToList();
        var expected = ratings.Average(r => r.Stars);
        _mockrepo.Setup(r => r.GetAverageRating("56")).ReturnsAsync(ratings);
        // Arrange
        var actual = await _sut.GetAverageRatingByEpisodeMediaIdAsync("56");
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task DeleteRatingByRatingIdAsync_ShouldThrow_WhenRatingIdIsInvalid()
    {
        // Arrange
        string expectedMessage = "Invalid rating id";

        // Act
        var actual = await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _sut.DeleteRatingAsync(Guid.NewGuid()));

        // Assert
        Assert.Equal(expectedMessage, actual.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task CreateRatingAsync_ShouldThrow_WhenEpisodeMediaIdIsNullOrWhiteSpace(string episodeMediaId)
    {
        // Arrange
        string expectedMessage = "Episode media id cannot be null or empty";

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateRatingAsync(3, episodeMediaId, Guid.NewGuid()));

        // Assert 
        Assert.Equal(expectedMessage, actual.Message);
    }
    
    
    
}
