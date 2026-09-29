using System.Text.Json.Serialization;

namespace TvSite.Infrastructure.DTO;

public class DiscoverMovieResult
{
    [JsonPropertyName("results")]
    public List<MediaDTO> Results { get; set; }
}