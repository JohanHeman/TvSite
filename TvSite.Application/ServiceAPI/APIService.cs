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
}