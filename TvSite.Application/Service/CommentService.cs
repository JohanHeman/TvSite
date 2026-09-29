using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities;
using TvSite.Domain.Interfaces.Services;
using TvSite.Infrastructure.Data;

namespace TvSite.Application.Service;

public class CommentService : ICommentService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Comment> _set;
    public CommentService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _set = dbContext.Comments;
    }

    public async Task<IReadOnlyList<Comment>> GetCommentsByMediaIdAsync(string mediaId)
    {
        return await _set.Include(c => c.ApplicationUser).Where(c => c.Id == mediaId).ToListAsync();
    }
    public async Task<Comment?> GetCommentByIdAsync(string commentId)
    {
        return await _set.Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public async Task CreateCommentAsync(Comment comment)
    {
        await _set.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteCommentAsync(Comment comment)
    {
        _set.Remove(comment);
        await _dbContext.SaveChangesAsync();
    }
}
