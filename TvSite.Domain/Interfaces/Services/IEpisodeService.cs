using TvSite.Domain.Entities.DbModels;

namespace TvSite.Domain.Interfaces.Services;

public interface IEpisodeService
{
    public Task CreateWatchedEpisode(WatchedEpisode watchedEpisode);

    public Task DeleteWatchedEpisode(WatchedEpisode watchedEpisode);
}
