using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.EntityFrameworkCore.Update;
using TvSite.Domain.Entities;
using TvSite.Domain.InterfacesAPI.Repositories;
using TvSite.Infrastructure.DTO;

namespace TvSite.Infrastructure.RepositoryAPI;

public class APIRepository : IAPIRepository
{
    private static readonly HttpClient _client = new HttpClient();
    
    
    public async Task<List<SearchResult>> GetTvShowsSearchResult(string title)
    {
        _client.DefaultRequestHeaders.Add("accept", "application/json");
        _client.DefaultRequestHeaders.Add("X-Api-Key", "0369759940beb53a4d992debcce65632");
        var apiKey = "0369759940beb53a4d992debcce65632";
        var searchResults = new List<SearchResult>();
        using (_client)
        {
            
            var endPoint = new Uri($"https://api.themoviedb.org/3/search/tv?query={Uri.EscapeDataString(title)}&include_adult=false&language=en-US&page=1&api_key={apiKey}");
        
            HttpResponseMessage response = await _client.GetAsync(endPoint);
            try
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseString = await response.Content.ReadAsStringAsync();
                    var results = JsonSerializer.Deserialize<TvShowSearchResultDTO>(responseString);
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
                throw new Exception("API could not get website");
            }
        }
        return searchResults;
    }
}