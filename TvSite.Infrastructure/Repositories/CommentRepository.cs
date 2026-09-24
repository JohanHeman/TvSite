using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using TvSite.Domain.Entities;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Infrastructure.Data;

namespace TvSite.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Comment> _set;
    public CommentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _set = dbContext.Comments;
    }
    
    public async Task<Comment?> GetCommentByIdAsync(string commentId)
    {
        return await _set.Include(c => c.ApplicationUser).FirstOrDefaultAsync(c => c.Id == commentId);
    }

    public async Task<IReadOnlyList<Comment>> GetCommentsByMediaIdAsync(string mediaId)
    {
        return await _set.Include(c => c.ApplicationUser).Where(c => c.Id == mediaId).ToListAsync();
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