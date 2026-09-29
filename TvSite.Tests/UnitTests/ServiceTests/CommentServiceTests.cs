using TvSite.Application.Service;
using TvSite.Domain.Entities;
using TvSite.Domain.Interfaces.Services;
using TvSite.Infrastructure.Data;
using TvSite.Tests.UnitTests.ClassData;

namespace TvSite.Tests.UnitTests.ServiceTests;

public class CommentServiceTests
{
    private readonly ICommentService _sut;

    public CommentServiceTests(ApplicationDbContext dbContext)
    {
        _sut = new CommentService(dbContext);
    }

    [Theory]
    [ClassData(typeof(CommentInvalidTextClassData))]
    public async Task CreateCommentAsync_ThrowsWhenTextIsNullOrEmpty(Comment comment)
    {
        await Assert.ThrowsAsync<Exception>( () => _sut.CreateCommentAsync(comment));
    }

    
}
