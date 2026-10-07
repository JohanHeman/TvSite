using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities.DbModels;
using TvSite.Domain.Interfaces.Services;
using TvSite.Infrastructure.Data;

namespace TvSite.Application.Service;

public class WatchedEpisodeService : IWatchedEpisodeService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<WatchedEpisode> _set;
    public WatchedEpisodeService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _set = dbContext.WatchedEpisodes;
    }

    public async Task<bool> GetIsWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId)
    {
        if (episodeMediaId == null) return false;

        WatchedEpisode? myEpisode;
        try
        {
            myEpisode = await _set
                .Where(episode => episode.EpisodeId == episodeMediaId)
                .Where(episode => episode.UserId == userId)
            .SingleOrDefaultAsync();

            if (myEpisode == null) return false;
        }
        catch (Exception ex)
        {
            throw new DbUpdateException("Could not get WatchedEpisode \nInner Exception: " + ex.InnerException);
        }

        return true;
    }

    public async Task CreateWatchedEpisode(string episodeMediaId, Guid userId)
    {
        if (userId == Guid.Empty || string.IsNullOrWhiteSpace(episodeMediaId)) return;

        var watchedEpisode = new WatchedEpisode()
        {
            Id = Guid.NewGuid(),
            EpisodeId = episodeMediaId,
            UserId = userId,
            DateTime = DateTime.Now,
            IsSoftDeleted = false,
        };

        try
        {
            await _set.AddAsync(watchedEpisode);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new DbUpdateException("Could not Save WatchedEpisode \nInner Exception: " + ex.InnerException);
        }
    }

    public async Task DeleteWatchedEpisode(string episodeMediaId, Guid userId)
    {
        if (episodeMediaId == null) return;

        WatchedEpisode? watchedEpisode;

        try
        {
            watchedEpisode = await _set
                .Where(episode => episode.EpisodeId == episodeMediaId)
                .Where(episode => episode.UserId == userId)
            .SingleOrDefaultAsync();

            if (watchedEpisode == null) return;

            _set.Remove(watchedEpisode);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new DbUpdateException("Could not delete WatchedEpisode \nInner Exception: " + ex.InnerException);
        }
    }
}
