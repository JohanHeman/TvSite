using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities.DbModels;
using TvSite.Domain.Interfaces.Services;
using TvSite.Infrastructure.Data;

namespace TvSite.Application.Service;

public class EpisodeService : IEpisodeService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<WatchedEpisode> _set;
    public EpisodeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _set = dbContext.WatchedEpisodes;
    }

    public async Task CreateWatchedEpisode(WatchedEpisode watchedEpisode)
    {
        if (watchedEpisode == null) return;

        try
        {
            await _set.AddAsync(watchedEpisode);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Could not Save WatchedEpisode \nInner Exception: " + ex.InnerException);
        }
    }

    public async Task DeleteWatchedEpisode(WatchedEpisode watchedEpisode)
    {
        if (watchedEpisode == null) return;

        try
        {
            _set.Remove(watchedEpisode);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex) 
        {
            throw new Exception("Could not delete WatchedEpisode \nInner Exception: " + ex.InnerException);
        }
    }
}
