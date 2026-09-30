using System.Text.Json;
using System.Text.Json.Serialization;
using TvSite.Domain.Entities;
using TvSite.Domain.InterfacesAPI.Repositories;
using TvSite.Infrastructure.DTO;

namespace TvSite.Infrastructure.RepositoryAPI;

public class APIRepository : IAPIRepository
{
    private readonly HttpClient _client;

    //Package: Microsoft.Extensions.Http (For IHttpClientFactory)
    public APIRepository(IHttpClientFactory httpFactory)
    {
        // Client config setup in Program.cs
        _client = httpFactory.CreateClient("TmdbApiClient");
    }
    
    
    public async Task<List<SearchResult>> GetTvShowsSearchResult(string title)
    {
        var searchResults = new List<SearchResult>();

        var endPoint = _client.BaseAddress + $"search/tv?query={Uri.EscapeDataString(title)}&include_adult=false&language=en-US&page=1";

        HttpResponseMessage response = await _client.GetAsync(endPoint);
        try
        {
            if (response.IsSuccessStatusCode)
            {
                string responseString = await response.Content.ReadAsStringAsync();
                var results = JsonSerializer.Deserialize<TvShowSearchResultDTO>(responseString);
                // Extract search results
                foreach (var show in results.Results)
                {
                    var searchResult = new SearchResult
                    {
                        MediaId = show.MediaId,
                        Title = show.Title
                    };
                    searchResults.Add(searchResult);
                }
            }
        }
        catch
        {
            throw new Exception("API could not get tv-list");
        }
        return searchResults;
    }

    public record DiscoverTvShowResultDTO([property: JsonPropertyName("results")]List<HomeScreenMediaDTO> Results);
    public async Task<List<DisplayMedia>> GetTvShowDiscoverListAsync()
    {
        var endpoint = _client.BaseAddress + "discover/tv?include_adult=false&include_null_first_air_dates=false&language=en-US&page=1&sort_by=popularity.desc";
        List<DisplayMedia> tvShows= new List<DisplayMedia>();
        var response = await _client.GetAsync(endpoint);
        if (response.IsSuccessStatusCode)
        {
            try
            {
                var responseString  = await response.Content.ReadAsStringAsync();
                var results = JsonSerializer.Deserialize<DiscoverTvShowResultDTO>(responseString);
                foreach (var show in results.Results)
                {
                    DisplayMedia media = new DisplayMedia
                    {
                        Id = show.Id.ToString(),
                        Name = show.Name,
                        Image = show.Image
                    };
                    tvShows.Add(media);
                }
            }
            catch (Exception e)
            {
                throw new Exception("API could not get discover movie-list");
            }
        }
        return tvShows;
    }
}