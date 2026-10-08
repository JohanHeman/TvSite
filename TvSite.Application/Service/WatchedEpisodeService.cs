using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;
using TvSite.Infrastructure.Data;

namespace TvSite.Application.Service;

public class WatchedEpisodeService : IWatchedEpisodeService
{
    private readonly IWatchedEpisodeRepository _repository;

    public WatchedEpisodeService(IWatchedEpisodeRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> IsWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId)
    {
        if (userId == Guid.Empty || string.IsNullOrWhiteSpace(episodeMediaId)) return false;

        try
        {
            return await _repository.IsWatchedEpisodeByUserAsync(episodeMediaId, userId);
        }
        catch (Exception ex)
        {
            throw new DbUpdateException("Could not get WatchedEpisode \nInner Exception: " + ex.InnerException);
        }
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
            await _repository.CreateWatchedEpisode(watchedEpisode);
        }
        catch (Exception ex)
        {
            throw new DbUpdateException("Could not Save WatchedEpisode \nInner Exception: " + ex.InnerException);
        }
    }

    public async Task DeleteWatchedEpisode(string episodeMediaId, Guid userId)
    {
        if (userId == Guid.Empty || string.IsNullOrWhiteSpace(episodeMediaId)) return;

        WatchedEpisode? watchedEpisode;
        try
        {
            watchedEpisode = await _repository.GetWatchedEpisodeByUserAsync(episodeMediaId, userId);
            if (watchedEpisode == null) return;

            await _repository.DeleteWatchedEpisode(watchedEpisode);
        }
        catch (Exception ex)
        {
            throw new DbUpdateException("Could not delete WatchedEpisode \nInner Exception: " + ex.InnerException);
        }
    }

    public async Task CreateOrDeleteWatchedEpisode(string episodeMediaId, Guid userId)
    {
        if (userId == Guid.Empty || string.IsNullOrWhiteSpace(episodeMediaId)) return;

        bool isExistingWatchedEpisode = await IsWatchedEpisodeByUserAsync(episodeMediaId, userId);

        if (!isExistingWatchedEpisode)
            await CreateWatchedEpisode(episodeMediaId, userId);

        else
            await DeleteWatchedEpisode(episodeMediaId, userId);
    }
}
