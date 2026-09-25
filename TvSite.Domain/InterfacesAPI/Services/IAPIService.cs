using TvSite.Domain.Entities;

namespace TvSite.Domain.InterfacesAPI.Services;

public interface IAPIService
{
    public Task<List<SearchResult>> GetMediasByTitle(string title);
}