using Moq;
using TvSite.Application.Service;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;

namespace TvSite.Tests.UnitTests.ServiceTests;

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
    public async Task GetAverageRatingByEpisodeMediaIdAsync_ShouldReturnAverageRating(int[] stars)
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
    public async Task GetAverageRatingByEpisodeMediaAsync_ShouldReturnAverageRating_WhenValidId()
    {
        // Arrange
        var mediaId = "4";

        var ratings = new List<Rating>()
        {
            new Rating
            {
                Id = Guid.NewGuid(),
                Stars = 5,
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = "episode-1"
            },
            new Rating
            {
                Id = Guid.NewGuid(),
                Stars = 4,
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = "episode-1"
            },
            new Rating
            {
                Id = Guid.NewGuid(),
                Stars = 3,
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = "episode-1"
            },
            new Rating
            {
                Id = Guid.NewGuid(),
                Stars = 2,
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = "episode-1"
            }
        };

        _mockrepo.Setup(r => r.GetAverageRating(mediaId)).ReturnsAsync(ratings);
        var expected = ratings.Average(r => r.Stars);
        // Act
        var actual = await _sut.GetAverageRatingByEpisodeMediaIdAsync(mediaId);


        // Assert
        _mockrepo.Verify(r => r.GetAverageRating(mediaId), Times.Once);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task GetRatingByIdAsync_ShouldThrow_WhenEpisodeMediaIdIsNullOrWhiteSpace(string? episodeMediaId)
    {
        //Arrange
        string expectedMessage = "Episode media id cannot be null or empty";

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.GetRatingByIdAsync(episodeMediaId, Guid.NewGuid()));

        // Assert 
        Assert.Equal(expectedMessage, actual.Message);
        _mockrepo.Verify(r => r.GetRatingByIdAsync(episodeMediaId, Guid.NewGuid()), Times.Never);
    }

    [Fact]
    public async Task GetRatingByIdAsync_ShouldReturnRating_WhenValidId()
    {
        // Arrange
        var rating = new Rating()
        {
            Id = Guid.NewGuid(),
            Stars = 1,
            EpisodeMediaId = "56",
            ApplicationUserId = Guid.NewGuid()
        };

        _mockrepo.Setup(r => r.GetRatingByIdAsync(rating.EpisodeMediaId, rating.Id)).ReturnsAsync(rating);
        // Act
        var actual = await _sut.GetRatingByIdAsync(rating.EpisodeMediaId, rating.Id);

        // Assert 
        Assert.NotNull(actual);
        Assert.Equal(rating.Id, actual.Id);
        _mockrepo.Verify(r => r.GetRatingByIdAsync(rating.EpisodeMediaId, rating.Id), Times.Once);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public async Task CreateRatingAsync_ShouldThrow_WhenEpisodeMediaIdIsNullOrWhiteSpace(string? episodeMediaId)
    {
        // Arrange
        string expectedMessage = "Episode media id cannot be null or empty";

        // Act
        var actual = await Assert.ThrowsAsync<ArgumentException>(
            () => _sut.CreateRatingAsync(3, episodeMediaId, Guid.NewGuid()));

        // Assert 
        Assert.Equal(expectedMessage, actual.Message);
    }

    [Fact]
    public async Task CreateRatingAsync_CallsRepoOnce_WhenValidProperties()
    {
        // Arrange
        Guid userId = Guid.NewGuid();
        string episodeMediaId = "4";
        int stars = 5;

        // Act
        await _sut.CreateRatingAsync(stars, episodeMediaId, userId);

        // Assert 
        _mockrepo.Verify(r => r.CreateRatingAsync(It.IsAny<Rating>()), Times.Once);
    }

    [Fact]
    public async Task UpdateRatingAsync_ShouldCallRepositoryOnceAndSetStars_WhenValidProperties()
    {
        // Arrange
        var rating = new Rating()
        {
            Id = Guid.NewGuid(),
            Stars = 5,
            EpisodeMediaId = "episode-1",
            ApplicationUserId = Guid.NewGuid()
        };
        int expeditedStars = 7;
        // Act

        await _sut.UpdateRatingAsync(rating, 7);

        // Assert
        _mockrepo.Verify(r => r.UpdateRatingAsync(It.IsAny<Rating>()), Times.Once);
        Assert.Equal(expeditedStars, rating.Stars);
    }

    [Fact]
    public async Task UpdateRatingAsync_ShouldNotCallRepository_WhenStarsAreUnchanged()
    {
        var rating = new Rating()
        {
            Id = Guid.NewGuid(),
            Stars = 5,
            EpisodeMediaId = "episode-1",
            ApplicationUserId = Guid.NewGuid()
        };

        await _sut.UpdateRatingAsync(rating, 5);

        _mockrepo.Verify(r => r.UpdateRatingAsync(It.IsAny<Rating>()), Times.Never);

    }



}
