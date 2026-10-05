using TvSite.Domain.Entities;

namespace TvSite.Domain.InterfacesAPI.Repositories;

public interface IAPIRepository
{
    public Task<List<SearchResult>> GetTvShowsSearchResult(string title);
    public Task<List<DisplayMedia>> GetTvShowDiscoverListAsync();
    public Task<TvSeries> GetTvShowDetails(string mediaId);
    public Task<TvSeriesSeason> GetSeasonDetails(string showId, int seasonNumber);
    public Task<TvSeriesEpisode> GetEpisodeDetailsAsync(string tvshowId, int seasonNumber, int episodeNumber);
}