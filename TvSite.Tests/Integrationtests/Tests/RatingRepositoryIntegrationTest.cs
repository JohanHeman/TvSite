using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Tests.Integrationtests.Setup;

namespace TvSite.Tests.Integrationtests.Tests;

public class RatingRepositoryIntegrationTest : ApplicationTestBase<Program>
{
    private readonly IRatingRepository _sut;

    public RatingRepositoryIntegrationTest(IntegrationTestFactory<Program> factory) : base(factory)
    {
        _sut = scope.ServiceProvider.GetRequiredService<IRatingRepository>();
    }

    [Fact]
    public async Task GetRatingByIdAsync_ShouldReturnRating_WhenValidIds()
    {
        await applicationDbContext.Database.EnsureDeletedAsync();
        await applicationDbContext.Database.EnsureCreatedAsync();

        // Arrange
        var userId = new Guid();
        var episodeMediaId = "mediaId";
        var expectedRating = new Rating()
        {
            Id = Guid.NewGuid(),
            Stars = 5,
            ApplicationUserId = userId,
            EpisodeMediaId = episodeMediaId
        };

        var user = new ApplicationUser()
        {
            Id = userId,
            DisplayName = "TestLover",
        };

        // Setup db to have existing data
        await applicationDbContext.AddAsync(expectedRating);
        await applicationDbContext.AddAsync(user);
        await applicationDbContext.SaveChangesAsync();

        // Act
        var actualRating = await _sut.GetRatingByIdAsync(episodeMediaId, userId);

        // Assert
        Assert.Equal(expectedRating.Id, actualRating!.Id);
        Assert.Equal(expectedRating.Stars, actualRating!.Stars);
        Assert.Equal(expectedRating.ApplicationUserId, actualRating!.ApplicationUserId);
        Assert.Equal(expectedRating.EpisodeMediaId, actualRating!.EpisodeMediaId);
    }

    [Fact]
    public async Task CreateRatingAsync_ShouldCreateRating_WhenValidRating()
    {
        await applicationDbContext.Database.EnsureDeletedAsync();
        await applicationDbContext.Database.EnsureCreatedAsync();

        // Arrange
        var ratingId = Guid.NewGuid();
        var newRating = new Rating()
        {
            Id = ratingId,
            Stars = 5,
            ApplicationUserId = Guid.NewGuid(),
            EpisodeMediaId = "ep5132"
        };

        // Act
        // Add rating to db
        await _sut.CreateRatingAsync(newRating);

        // Get rating from db
        var rating = await applicationDbContext.Set<Rating>().SingleOrDefaultAsync(rating => rating.Id == ratingId);

        // Assert
        Assert.NotNull(rating);
    }
}
