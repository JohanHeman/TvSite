using TvSite.Domain.Entities;

namespace TvSite.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    public Task<Comment?> GetCommentByIdAsync(Guid commentId);
    public Task<IReadOnlyList<Comment>> GetCommentsByMediaIdAsync(string mediaId);
    public Task CreateCommentAsync(string commentText, string episodeMediaId, Guid userId);
    public Task DeleteCommentAsync(Comment comment);
}