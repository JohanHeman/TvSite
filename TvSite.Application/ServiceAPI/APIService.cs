using TvSite.Domain.Entities.Api;
using TvSite.Domain.InterfacesAPI.Repositories;
using TvSite.Domain.InterfacesAPI.Services;

namespace TvSite.Application.ServiceAPI;

public class APIService : IAPIService
{
    private readonly IAPIRepository _repository;

    public APIService(IAPIRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SearchResult>> GetTvShowsSearchResultByTitle(string title)
    {
        return await _repository.GetTvShowsSearchResult(title);
    }

    public async Task<List<DisplayMedia>> GetTvShowsFromDiscover()
    {
        return await _repository.GetTvShowDiscoverListAsync();
    }

    public async Task<TvSeries> GetTvShowDetailsById(string tvShowId)
    {
        if (string.IsNullOrWhiteSpace(tvShowId))
            return null!;

        return await _repository.GetTvShowDetails(tvShowId);
    }

    public async Task<TvSeriesSeason> GetSeasonDetails(string tvShowId, int seasonNumber)
    {
        if (string.IsNullOrWhiteSpace(tvShowId))
            return null!;

        return await _repository.GetSeasonDetails(tvShowId, seasonNumber);
    }

    public async Task<TvSeriesEpisode> GetEpisodeDetails(string tvShowId, int seasonNumber, int episodeNumber)
    {
        if (string.IsNullOrWhiteSpace(tvShowId))
            return null!;

        return await _repository.GetEpisodeDetailsAsync(tvShowId, seasonNumber, episodeNumber);
    }
}
