using TvSite.Domain.Entities;

namespace TvSite.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    // Get comments
    public Task<Comment> GetCommentByIdAsync(string commentId);
    public Task<IReadOnlyList<Comment>> GetCommentsByMediaIdAsync(string mediaId);

    // Create update delete
    public Task CreateCommentAsync(Comment comment);
    public Task DeleteCommentAsync(Comment comment);
    
}
