using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Infrastructure.Data;

namespace TvSite.Infrastructure.Repositories;

public class RatingRepository : IRatingRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<Rating> _set;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;
    public RatingRepository(ApplicationDbContext dbContext, IDbContextFactory<ApplicationDbContext> dbContextFactory)
    {
        _dbContext = dbContext;
        _set = dbContext.Ratings;
        _dbContextFactory = dbContextFactory;
    }

    public async Task CreateRatingAsync(Rating rating)
    {
        await _set.AddAsync(rating);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteRatingAsync(Rating rating)
    {
        _set.Remove(rating);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateRatingAsync(Rating rating)
    {
        _set.Update(rating);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Rating?> GetRatingByIdAsync(string episodeMediaId, Guid userId)
    {
        return await _set.FirstOrDefaultAsync(r => r.EpisodeMediaId == episodeMediaId && r.ApplicationUserId == userId);
    }

    public async Task<Rating?> GetRatingByRatingIdAsync(Guid ratingId)
    {
        return await _set.FirstOrDefaultAsync(r => r.Id == ratingId); 
    }

    public async Task<List<Rating>> GetAverageRating(string episodeMediaId)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        return await dbContext.Ratings.Where(s => s.EpisodeMediaId == episodeMediaId).ToListAsync();
    }
}
