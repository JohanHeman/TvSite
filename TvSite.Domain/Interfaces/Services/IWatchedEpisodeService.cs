using TvSite.Domain.Entities.DbModels;

namespace TvSite.Domain.Interfaces.Services;

public interface IWatchedEpisodeService
{
    public Task CreateWatchedEpisode(string episodeMediaId, Guid userId);
    public Task DeleteWatchedEpisode(string episodeMediaId, Guid userId);
    public Task<bool> GetIsWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId);
}
