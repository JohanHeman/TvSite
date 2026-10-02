using Moq;
using TvSite.Application.Service;
using TvSite.Domain.Entities;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;
using TvSite.Infrastructure.Data;

namespace TvSite.Tests.UnitTests.ApplicationTests;

public class RatingServiceTests
{

    private readonly IRatingService _ratingService;
    private readonly Mock<IRatingRepository> _mockrepo;
    public RatingServiceTests()
    {
        _mockrepo = new Mock<IRatingRepository>();
        _ratingService = new RatingService(_mockrepo.Object);
    }


    [Theory]
    [InlineData(new int[] { 4, 4, 5, 6, 7 })]
    [InlineData(new int[] { 1, 2, 3, 4, 5 })]
    [InlineData(new int[] { 5, 5, 5, 5, 5 })]
    [InlineData(new int[] { 1, 1, 2, 2, 3 })]
    public async Task GetAverageRatingByMediaIdAsync_ShouldReturnAverageRating(int[] input)
    {
        var ratings = input.Select(star => new Rating { Stars = star }).ToList();
        var expected = ratings.Average(r => r.Stars);
        _mockrepo.Setup(r => r.GetAverageRating("56")).ReturnsAsync(ratings);
        // Arrange
        var actual = await _ratingService.GetAverageRatingByMediaIdAsync("56");
        // Assert
        Assert.Equal(expected, actual);
    }

}
