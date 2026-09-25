using System.Text.Json.Serialization;

namespace TvSite.Infrastructure.DTO;

public class TvShowSearchResult
{
    //https://developer.themoviedb.org/reference/search-tv
    [JsonPropertyName("mediaId")]
    public string MediaId{ get; set; }
    [JsonPropertyName("name")]
    public string Title{ get; set; }
}