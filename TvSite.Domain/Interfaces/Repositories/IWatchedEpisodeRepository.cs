namespace TvSite.Domain.Interfaces.Repositories;

public interface IWatchedEpisodeRepository
{
    public Task CreateWatchedEpisode(string episodeMediaId, Guid userId);
    public Task DeleteWatchedEpisode(string episodeMediaId, Guid userId);
    public Task<bool> IsWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId);
    public Task CreateOrDeleteWatchedEpisode(string episodeMediaId, Guid userId);
}