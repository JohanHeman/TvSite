using TvSite.Domain.Interfaces.Repositories;

namespace TvSite.Infrastructure.Repositories;

public class WatchedEpisodeRepository : IWatchedEpisodeRepository
{
    public Task CreateWatchedEpisode(string episodeMediaId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteWatchedEpisode(string episodeMediaId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task CreateOrDeleteWatchedEpisode(string episodeMediaId, Guid userId)
    {
        throw new NotImplementedException();
    }
}