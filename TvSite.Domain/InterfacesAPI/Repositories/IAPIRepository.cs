using TvSite.Domain.Entities;

namespace TvSite.Domain.InterfacesAPI.Repositories;

public interface IAPIRepository
{
    public Task<List<SearchResult>> GetTvShowsSearchResult(string title);
}