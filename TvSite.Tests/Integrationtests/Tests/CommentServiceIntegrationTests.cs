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

        // Act
        await _sut.CreateCommentAsync(text, episodeMediaId, userId);
        var comment = applicationDbContext.Comments.Where(c => c.Text == text
        && c.EpisodeMediaId == episodeMediaId && c.ApplicationUserId == userId).FirstOrDefault();
        // Assert
        Assert.NotNull(comment);

    }



}