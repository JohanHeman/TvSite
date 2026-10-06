using TvSite.Domain.Entities.DbModels;

namespace TvSite.Domain.Interfaces.Services;

public interface IWatchedEpisodeService
{
    public Task CreateWatchedEpisode(WatchedEpisode watchedEpisode);
    public Task DeleteWatchedEpisode(WatchedEpisode watchedEpisode);
    public Task<bool> GetIsWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId);
}
