using TvSite.Domain.Entities.Database;

namespace TvSite.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    public Task<Comment?> GetCommentByIdAsync(Guid commentId);
    public Task<IReadOnlyList<Comment>> GetCommentsByMediaIdAsync(string episodeMediaId);
    public Task CreateCommentAsync(Comment comment);
    public Task DeleteCommentAsync(Comment comment);
}
