using TvSite.Domain.Entities.Api;
using TvSite.Domain.Entities.Database;

namespace TvSite.Domain.Interfaces.Services;

public interface IWatchedEpisodeService
{
    public Task CreateWatchedEpisode(string episodeMediaId, Guid userId, string tvSeriesId, int seasonNumber, int episodeNumber);
    public Task DeleteWatchedEpisode(string episodeMediaId, Guid userId);
    public Task<bool> IsWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId);
    public Task CreateOrDeleteWatchedEpisode(string episodeMediaId, Guid userId, string tvSeriesId, int seasonNumber, int episodeNumber);
    //public Task<List<TvSeriesEpisode>> GetWatchedEpisodesByUserIdAsync(Guid userId);
}
