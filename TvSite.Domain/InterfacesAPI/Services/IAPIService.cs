using TvSite.Domain.Entities;

namespace TvSite.Domain.InterfacesAPI.Services;

public interface IAPIService
{
    public Task<List<SearchResult>> GetMediasByTitle(string title);
    public Task<List<DisplayMedia>> GetTvShowsFromDiscover();
    public Task<TvSeries> GetTvShowDetailsById(string mediaId);
    public Task<TvSeriesSeason> GetSeasonDetails(string showId, int seasonNumber);
    public Task<TvSeriesEpisode> GetEpisodeDetails(string tvshowId, int seasonNumber, int episodeNumber);
}