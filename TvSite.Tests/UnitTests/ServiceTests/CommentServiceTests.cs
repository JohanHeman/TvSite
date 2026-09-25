using TvSite.Application.Service;
using TvSite.Domain.Entities;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;
using TvSite.Tests.UnitTests.ClassData;

namespace TvSite.Tests.UnitTests.ServiceTests;

public class CommentServiceTests
{
    private readonly ICommentService _sut;
    private readonly ICommentRepository _commentRepository;

    public CommentServiceTests()
    {
        _sut = new CommentService(_commentRepository);
    }

    [Theory]
    [ClassData(typeof(CommentInvalidTextClassData))]
    public async Task CreateCommentAsync_ThrowsWhenTextIsNullOrEmpty(Comment comment)
    {
        await Assert.ThrowsAsync<Exception>( () => _sut.CreateCommentAsync(comment));
    }

    
}
