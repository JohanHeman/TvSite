using TvSite.Domain.Entities;

namespace TvSite.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    // Get comments
    public Task GetCommentByIdAsync(string commentId);
    public Task GetCommentsByMediaIdAsync(string mediaId);
    public Task GetCommentsByUserIdAsync(string userId);

    // Create update delete
    public Task CreateCommentAsync(Comment comment);
    public Task UpdateCommentAsync(Comment comment);
    public Task DeleteCommentAsync(Comment comment);
    
}
