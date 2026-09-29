using System.Text.Json;
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

    public async Task<List<Media>> GetMovieDiscoverListAsync()
    {
        var endpoint = _client.BaseAddress + "discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc";
        List<Media> movies = new List<Media>();
        var response = await _client.GetAsync(endpoint);
        if (response.IsSuccessStatusCode)
        {
            try
            {
                var responseString  = await response.Content.ReadAsStringAsync();
                var results = JsonSerializer.Deserialize<DiscoverMovieResult>(responseString);
                foreach (var movie in results.Results)
                {
                    Media media = new Media
                    {
                        Id = movie.Id.ToString(),
                        Title = movie.Title,
                        Description = movie.Description,
                        Directors = movie.Directors,
                        Actors = movie.Actors,
                        AirDate =  movie.AirDate,
                        MediaImage = movie.MediaImage
                    };
                    movies.Add(media);
                }
            }
            catch (Exception e)
            {
                throw new Exception("API could not get discover movie-list");
            }
        }
        return movies;
    }
}