using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Infrastructure.Data;
using TvSite.Presentation.Components;
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
        var userId = Guid.NewGuid();
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
    public async Task GetRatingByRatingIdAsync_ShouldReturnRating_WhenValidId()
    {

        await applicationDbContext.Database.EnsureDeletedAsync();
        await applicationDbContext.Database.EnsureCreatedAsync();

        // Arrange
        var ratingId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var episodeMediaId = "mediaId";
        var expectedRating = new Rating()
        {
            Id = ratingId,
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
        var actualRating = await _sut.GetRatingByRatingIdAsync(ratingId);

        // Assert
        Assert.Equal(expectedRating.Id, actualRating!.Id);
        Assert.Equal(expectedRating.Stars, actualRating!.Stars);
        Assert.Equal(expectedRating.ApplicationUserId, actualRating!.ApplicationUserId);
        Assert.Equal(expectedRating.EpisodeMediaId, actualRating!.EpisodeMediaId);
    }

    [Fact]
    public async Task GetRatingByRatingIdAsync_ShouldReturnNull_WhenIdDoesNotExistInDb()
    {
        await applicationDbContext.Database.EnsureDeletedAsync();
        await applicationDbContext.Database.EnsureCreatedAsync();

        // Act
        var actualRating = await _sut.GetRatingByRatingIdAsync(Guid.NewGuid());

        // Assert
        Assert.Null(actualRating);
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


    //Test does not work. Not sure why? Because of DbContextFactory?
    //This test works on singel run but not when all tests run. hmmmmm
    // Seems to be due to test running at the same time
    /*
    [Fact]
    public async Task GetRatingsByEpisodeMediaId()
    {
        //Repo method
        //await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        //return await dbContext.Ratings.Where(s => s.EpisodeMediaId == episodeMediaId).ToListAsync();

        await applicationDbContext.Database.EnsureDeletedAsync();
        await applicationDbContext.Database.EnsureCreatedAsync();

        // Arrange
        var expectedAmount = 3;
        var epId = "epId";
        var ratings = new List<Rating>()
        {
            new()
            {
                Id = Guid.NewGuid(),
                Stars = 5,
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = epId
            },
            new()
            {
                Id = Guid.NewGuid(),
                Stars = 5,
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = "differentId"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Stars = 3,
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = epId
            },
            new()
            {
                Id = Guid.NewGuid(),
                Stars = 5,
                ApplicationUserId = Guid.NewGuid(),
                EpisodeMediaId = epId
            }
        };

        // Save data to db
        await applicationDbContext.AddRangeAsync(ratings);
        await applicationDbContext.SaveChangesAsync();

        // Act
        var actual = await _sut.GetRatingsByEpisodeMediaId(epId);

        // Assert
        Assert.Equal(expectedAmount, actual.Count);
    }
    */
}
