using TvSite.Domain.Entities;
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
    
    
    public async Task<List<SearchResult>> GetMediasByTitle(string title)
    {
        return await _repository.GetTvShowsSearchResult(title);
    }

    public async Task<List<DisplayMedia>> GetTvShowsFromDiscover()
    {
        return await _repository.GetTvShowDiscoverListAsync();
    }

    public async Task<Media> GetTvShowDetailsById(string mediaId)
    {
        if (string.IsNullOrWhiteSpace(mediaId))
            return null!;
        
        return await _repository.GetTvShowDetails(mediaId);
    }

    public async Task<TvSeriesSeason> GetSeasonDetails(string seriesId, int seasonNumber)
    {
        if (string.IsNullOrWhiteSpace(seriesId))
            return null!;

        return await _repository.GetSeasonDetails(seriesId, seasonNumber);
    }
}