using System.Text.Json.Serialization;

namespace TvSite.Infrastructure.DTO;

public class MediaDTO
{
    
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("overview")]
    public string Description { get; set; }

    [JsonPropertyName("directors")]
    public string[] Directors { get; set; }

    [JsonPropertyName("actors")]
    public string[] Actors { get; set; }

    [JsonPropertyName("release_date")]
    public DateOnly AirDate { get; set; }

    [JsonPropertyName("poster_path")]
    public string MediaImage { get; set; }
}
