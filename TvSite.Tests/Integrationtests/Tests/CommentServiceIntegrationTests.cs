using Microsoft.Extensions.DependencyInjection;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Infrastructure.Data;
using TvSite.Tests.Integrationtests.Setup;

namespace TvSite.Tests.Integrationtests.Tests;

public class CommentServiceIntegrationTests : IClassFixture<IntegrationTestFactory<Program>>
{
    private readonly ICommentRepository _sut;
    private readonly TestData _testData = new TestData();
    internal ApplicationDbContext applicationDbContext;
    internal readonly IServiceScope scope;


    public CommentServiceIntegrationTests(IntegrationTestFactory<Program> factory)
    {
        scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        applicationDbContext.Database.EnsureCreated();

        // gets required service (sut) from the registered Services for DI and registered dependencies are automatically injected 
        _sut = scope.ServiceProvider.GetRequiredService<ICommentRepository>();
        applicationDbContext.Database.EnsureDeleted();
        applicationDbContext.Database.EnsureCreated();

    }

    [Fact]
    public async Task CreateCommentAsync_ShouldCreateComment_WhenCommentValid()
    {
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
        // Arrange
        var users = _testData.GetTestUsers();
        var comments = _testData.GetTestCommentsAsync();

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
        var users = _testData.GetTestUsers();
        var comments = _testData.GetTestCommentsAsync();
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