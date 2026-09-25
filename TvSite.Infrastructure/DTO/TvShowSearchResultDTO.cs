using System.Text.Json.Serialization;

namespace TvSite.Infrastructure.DTO;

public class TvShowSearchResultDTO
{
    [JsonPropertyName("results")]
    public List<TvShowSearchResult> Results { get; set; }
}