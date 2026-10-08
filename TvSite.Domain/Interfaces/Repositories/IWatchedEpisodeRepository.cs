using TvSite.Domain.Entities.DbModels;

namespace TvSite.Domain.Interfaces.Repositories;

public interface IWatchedEpisodeRepository
{
    public Task CreateWatchedEpisode(WatchedEpisode episode);
    public Task DeleteWatchedEpisode(WatchedEpisode episode);
    public Task<bool> IsWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId);
    public Task<WatchedEpisode?> GetWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId);
}