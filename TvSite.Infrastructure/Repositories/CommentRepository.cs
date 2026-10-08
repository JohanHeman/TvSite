using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Infrastructure.Data;

namespace TvSite.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{

    private readonly ApplicationDbContext _dbContext;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    private readonly DbSet<Comment> _set;
    public CommentRepository(ApplicationDbContext dbContext, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContext = dbContext;
        _set = dbContext.Comments;
        _dbContextFactory = dbContextFactory;
    }

    public async Task<IReadOnlyList<Comment>> GetCommentsByMediaIdAsync(string episodeMediaId)
    {
        await using var context = await _dbContextFactory.CreateDbContextAsync();
        return await context.Comments.Include(c => c.ApplicationUser).Where(c => c.EpisodeMediaId == episodeMediaId).ToListAsync();
    }
    public async Task<Comment?> GetCommentByIdAsync(Guid commentId)
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
