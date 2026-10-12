using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TvSite.Application.Service;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;
using TvSite.Tests.Integrationtests.Setup;

namespace TvSite.Tests.Integrationtests.Tests;

public class CommentServiceIntegrationTests : ApplicationTestBase<Program>
{
    private readonly ICommentService _sut;

    public CommentServiceIntegrationTests(IntegrationTestFactory<Program> factory) : base(factory)
    {
        // gets required service (sut) from the registered Services for DI and registered dependencies are automatically injected 
        _sut = scope.ServiceProvider.GetRequiredService<ICommentService>();
    }

    [Fact]
    public async Task CreateCommentAsync_ShouldCreateComment_WhenCommentValid()
    {
        // Arrange
        string text = "comment text";
        string episodeMediaId = "3";
        Guid userId = Guid.NewGuid();
        DateTime dateTime = DateTime.Now;

        // Act
        await _sut.CreateCommentAsync(text, episodeMediaId, userId, dateTime);
        var comment = applicationDbContext.Comments.Where(c => c.Text == text
        && c.EpisodeMediaId == episodeMediaId && c.ApplicationUserId == userId).FirstOrDefault();
        // Assert
        Assert.NotNull(comment);

    }

    [Fact]
    public async Task GetCommentsByEpisodeMediaIdAsync_ShouldReturnComments_WhenCommentsExist()
    {
        // Arrange
        var userIds = new[]
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        };

        var users = userIds.Select(id => new ApplicationUser
        {
            Id = id,
            UserName = $"user-{id}",
            NormalizedUserName = $"USER-{id}".ToUpperInvariant(),
            Email = $"{id}@example.com",
            NormalizedEmail = $"{id}@example.com".ToUpperInvariant(),
            DisplayName = "Test User"
        });
        var comments = new List<Comment>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Text = "Great episode!",
                DateTime = DateTime.Now.AddMinutes(-20),
                ApplicationUserId = userIds[0],
                EpisodeMediaId = "3"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "The ending was amazing.",
                DateTime = DateTime.Now.AddMinutes(-15),
                ApplicationUserId = userIds[1],
                EpisodeMediaId = "3"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "I cannot wait for the next episode.",
                DateTime = DateTime.Now.AddMinutes(-10),
                ApplicationUserId = userIds[2],
                EpisodeMediaId = "3"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "The actors did a great job.",
                DateTime = DateTime.Now.AddMinutes(-5),
                ApplicationUserId = userIds[3],
                EpisodeMediaId = "3"
            }
        };
        await applicationDbContext.AddRangeAsync(users);
        await applicationDbContext.Comments.AddRangeAsync(comments);
        await applicationDbContext.SaveChangesAsync();
 
        // Act
        var actual =  await _sut.GetCommentsByEpisodeMediaIdAsync("3");

        // Assert 
        Assert.Equal(comments.Count, actual.Count);
    }


}
