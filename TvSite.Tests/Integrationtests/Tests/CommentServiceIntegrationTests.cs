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
    private readonly ICommentRepository _sut;

    public CommentServiceIntegrationTests(IntegrationTestFactory<Program> factory) : base(factory)
    {
        // gets required service (sut) from the registered Services for DI and registered dependencies are automatically injected 
        _sut = scope.ServiceProvider.GetRequiredService<ICommentRepository>();
    }

    [Fact]
    public async Task CreateCommentAsync_ShouldCreateComment_WhenCommentValid()
    {
        await applicationDbContext.Database.EnsureDeletedAsync();
        await applicationDbContext.Database.EnsureCreatedAsync();
        
        // Arrange
        string text = "comment text";
        string episodeMediaId = "3";
        Guid userId = Guid.NewGuid();
        DateTime dateTime = DateTime.Now;
        var newComment = new Comment()
        {
            Id = Guid.NewGuid(),
            Text = text,
            DateTime = dateTime,
            ApplicationUserId = userId,
            EpisodeMediaId = episodeMediaId,
        };

        // Act
        await _sut.CreateCommentAsync(newComment);
        var comment = applicationDbContext.Comments.Where(c => c.Text == text
        && c.EpisodeMediaId == episodeMediaId && c.ApplicationUserId == userId).FirstOrDefault();
        // Assert
        Assert.NotNull(comment);

    }

    [Fact]
    public async Task GetCommentsByEpisodeMediaIdAsync_ShouldOnlyReturnCommentsWithEpisodeMediaId_WhenCommentsExist()
    {
        await applicationDbContext.Database.EnsureDeletedAsync();
        await applicationDbContext.Database.EnsureCreatedAsync();
        // Arrange
        var users = TestData.GetTestUsers();
        var comments = TestData.GetTestCommentsAsync();

        // itterate through comments and users to set the id from user to comment
        foreach (var (comment, user) in comments.Zip(users))
        {
            comment.ApplicationUserId = user.Id;
        }

        var expected = 3;

        await applicationDbContext.AddRangeAsync(users);
        await applicationDbContext.Comments.AddRangeAsync(comments);
        await applicationDbContext.SaveChangesAsync();

        // Act
        var actual = await _sut.GetCommentsByMediaIdAsync("3");

        // Assert 
        Assert.Equal(expected, actual.Count);
    }


    [Fact]
    public async Task GetCommentsByUserIdAsync_ShouldReturnEmptyList_WhenNoMatchingIds()
    {
        // Arrange
        var users = TestData.GetTestUsers();
        var comments = TestData.GetTestCommentsAsync();
        await applicationDbContext.AddRangeAsync(users);
        await applicationDbContext.Comments.AddRangeAsync(comments);

        // Act
        var actual = await _sut.GetCommentsByMediaIdAsync("56");

        // Assert
        Assert.Empty(actual);
    }

    [Fact]
    public async Task GetCommentByIdAsync_ShouldReturnComment_WhenValidId()
    {
        await  applicationDbContext.Database.EnsureDeletedAsync();
        await applicationDbContext.Database.EnsureCreatedAsync();
        
        // Arrange
        var comment = new Comment()
        {
            Id = Guid.NewGuid(),
            Text = "Great episode!",
            EpisodeMediaId = "3"
        };

        var user = new ApplicationUser()
        {
            Id = Guid.NewGuid(),
            UserName = $"user-{comment.Id}",
            DisplayName = "Test User"
        };

        comment.ApplicationUserId = user.Id;

        await applicationDbContext.AddAsync(user);
        await applicationDbContext.Comments.AddAsync(comment);
        await applicationDbContext.SaveChangesAsync();
        // Act
        var actual = await _sut.GetCommentByIdAsync(comment.Id);

        // Assert 
        Assert.Equal(comment.Text, actual.Text);
        Assert.Equal(comment.EpisodeMediaId, actual.EpisodeMediaId);
        Assert.Equal(comment.Id, actual.Id);
    }

}

internal class TestData
{
    public static List<ApplicationUser> GetTestUsers()
    {
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
        return users.ToList();
    }


    public static List<Comment> GetTestCommentsAsync()
    {
        var comments = new List<Comment>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Text = "Great episode!",
                DateTime = DateTime.Now.AddMinutes(-20),
                EpisodeMediaId = "3"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "The ending was amazing.",
                DateTime = DateTime.Now.AddMinutes(-15),
                EpisodeMediaId = "3"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "I cannot wait for the next episode.",
                DateTime = DateTime.Now.AddMinutes(-10),
                EpisodeMediaId = "3"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Text = "The actors did a great job.",
                DateTime = DateTime.Now.AddMinutes(-5),
                EpisodeMediaId = "8"
            }
        };
        return comments;
    }
}
