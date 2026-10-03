using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using System.Text.Json.Serialization;
using TvSite.Domain.Entities;
using TvSite.Domain.InterfacesAPI.Repositories;
using TvSite.Infrastructure.DTO;
using TvSite.Infrastructure.DTO.SeasonDetailsDTO;

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

    public record TvShowSearchResultDTO([property: JsonPropertyName("results")] List<TvShowSearchResult> Results);
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
                        MediaId = show.MediaId.ToString(),
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

    public record DiscoverTvShowResultDTO([property: JsonPropertyName("results")] List<HomeScreenMediaDTO> Results);
    public async Task<List<DisplayMedia>> GetTvShowDiscoverListAsync()
    {
        var baseImgUrl = "https://image.tmdb.org/t/p/w154";
        var endpoint = _client.BaseAddress + "discover/tv?include_adult=false&include_null_first_air_dates=false&language=en-US&page=1&sort_by=popularity.desc";
        List<DisplayMedia> tvShows = new List<DisplayMedia>();
        var response = await _client.GetAsync(endpoint);
        if (response.IsSuccessStatusCode)
        {
            try
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var results = JsonSerializer.Deserialize<DiscoverTvShowResultDTO>(responseString);
                foreach (var show in results.Results)
                {
                    DisplayMedia media = new DisplayMedia
                    {
                        Id = show.Id.ToString(),
                        Name = show.Name,
                        Image = baseImgUrl + show.Image
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

    public async Task<TvSeries> GetTvShowDetails(string mediaId)
    {
        var media = new TvSeries();
        var baseImgUrlTvSeries = "https://image.tmdb.org/t/p/w154";
        var baseImgUrlSeason = "https://image.tmdb.org/t/p/w92";

        var endpoint = _client.BaseAddress + $"tv/{mediaId}";
        var response = await _client.GetAsync(endpoint);

        try
        {
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var tvShowDetails = JsonSerializer.Deserialize<TvShowDetailsDTO>(responseString);

                if (tvShowDetails != null)
                {
                    media.Id = tvShowDetails.Id.ToString();
                    media.Name = tvShowDetails.Title;
                    media.Description = tvShowDetails.Description;
                    media.Directors = tvShowDetails.Directors.Select(director => director.Name).ToArray();
                    media.MediaImage = baseImgUrlTvSeries + tvShowDetails.ImagePath;
                    media.AirDate = tvShowDetails.AirDate;

                    media.Seasons = tvShowDetails.Seasons.Select(season => new TvSeriesSeason()
                    {
                        MediaId = season.Id.ToString(),
                        SeasonNumber = season.SeasonNumber,
                        SeasonName = season.Title,
                        ImagePath = baseImgUrlSeason + season.ImagePath,
                        Description = season.Description,
                        EpisodeCount = season.EpisodeCount,
                        AirDate = season.AirDate,
                    }).ToList();
                }
            }
        }
        catch { throw new Exception("Api could not get TvShowDetails"); }

        return media;
    }


    public async Task<TvSeriesSeason> GetSeasonDetails(string showId, int seasonNumber)
    {
        var tvSeriesSeason = new TvSeriesSeason();
        var baseImgUrlTvSeason = "https://image.tmdb.org/t/p/w154";
        var baseImgUrlEpisode = "https://image.tmdb.org/t/p/w92";

        var endPoint = _client.BaseAddress + $"tv/{showId}/season/{seasonNumber}";
        var response = await _client.GetAsync(endPoint);

        try
        {
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var season = JsonSerializer.Deserialize<SeasonDetailsDTO>(responseString);

                if (season != null)
                {
                    tvSeriesSeason.MediaId = season.Id.ToString();
                    tvSeriesSeason.SeasonName = season.Title;
                    tvSeriesSeason.Description = season.Description;
                    tvSeriesSeason.SeasonNumber = season.SeasonNumber;
                    tvSeriesSeason.ImagePath = baseImgUrlTvSeason + season.ImagePath;
                    tvSeriesSeason.AirDate = season.AirDate;

                    tvSeriesSeason.Episodes = season.Episodes.Select(episode => new TvSeriesEpisode()
                    {
                        Id = episode.Id.ToString(),
                        Title = episode.Title,
                        Description = episode.Description,
                        AirDate = episode.AirDate,
                        EpisodeNumber = episode.EpisodeNumber,
                        ImagePath = baseImgUrlEpisode + episode.ImagePath
                    }).ToList();
                }
            }
        }
        catch { throw new Exception("Api could not get TvSeasonDetails"); }

        return tvSeriesSeason;
    }

    public async Task<TvSeriesEpisode> GetEpisodeDetailsAsync(string tvshowId, int seasonNumber, int episodeNumber)
    {

        var tvSeriesEpisode = new TvSeriesEpisode();
        var endpoint = _client.BaseAddress +  $"tv/{tvshowId}/season/{seasonNumber}/episode/{episodeNumber}?language=en-US";
        
        var response = await _client.GetAsync(endpoint);

        try
        {
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var episode = JsonSerializer.Deserialize<SeasonEpisodeDTO>(responseString);

                if (episode != null)
                {
                    tvSeriesEpisode.Id = episode.Id.ToString();
                    tvSeriesEpisode.Title = episode.Title;
                    tvSeriesEpisode.Description = episode.Description;
                    tvSeriesEpisode.AirDate = episode.AirDate;
                    tvSeriesEpisode.EpisodeNumber = episode.EpisodeNumber;
                    tvSeriesEpisode.ImagePath = episode.ImagePath;
                }
            }
        }
        catch
        {
            throw new Exception("Api could not get TvEpisodeDetails");
        }
        return tvSeriesEpisode;
    }
}