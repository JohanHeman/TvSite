using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TvSite.Infrastructure.DTO.SeasonDetailsDTO
{
    public class SeasonEpisodeDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Title { get; set; }

        [JsonPropertyName("overview")]
        public string Description { get; set; }

        [JsonPropertyName("air_date")]
        public DateOnly AirDate { get; set; }

        [JsonPropertyName("episode_number")]
        public int EpisodeNumber { get; set; }

        [JsonPropertyName("still_path")]
        public string ImagePath { get; set; }
    }
}
