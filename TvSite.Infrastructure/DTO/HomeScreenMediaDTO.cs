using System.Text.Json.Serialization;

namespace TvSite.Infrastructure.DTO;

public class HomeScreenMediaDTO
{
    
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("poster_path")]
    public string Image { get; set; }
}
