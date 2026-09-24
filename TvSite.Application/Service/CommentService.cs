using TvSite.Domain.Entities;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;

namespace TvSite.Application.Service;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public Task<IReadOnlyList<Comment>> GetCommentsByMediaIdAsync(string mediaId)
    {
        throw new NotImplementedException();
    }
    public Task<Comment?> GetCommentByIdAsync(string commentId)
    {
        throw new NotImplementedException();
    }

    public async Task CreateCommentAsync(Comment comment)
    {
        if (string.IsNullOrWhiteSpace(comment.Text))
            throw new Exception("The comment must have a text");

        await _commentRepository.CreateCommentAsync(comment);
    }

    public Task DeleteCommentAsync(Comment comment)
    {
        throw new NotImplementedException();
    }
}
