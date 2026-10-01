using TvSite.Domain.Entities;

namespace TvSite.Domain.InterfacesAPI.Repositories;

public interface IAPIRepository
{
    public Task<List<SearchResult>> GetTvShowsSearchResult(string title);
    public Task<List<DisplayMedia>> GetTvShowDiscoverListAsync();
    public Task<Media> GetTvShowDetails(string mediaId);
    public Task<TvSeriesSeason> GetSeasonDetails(string seriesId, int seasonNumber);
}