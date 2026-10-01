using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace TvSite.Infrastructure.DTO.SeasonDetailsDTO
{
    public class SeasonDetailsDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("season_number")]
        public int SeasonNumber { get; set; }

        [JsonPropertyName("overview")]
        public string Description { get; set; }

        [JsonPropertyName("air_date")]
        public DateOnly DateOnly { get; set; }

        [JsonPropertyName("poster_path")]
        public string ImagePath { get; set; }

        [JsonPropertyName("episodes")]
        public List<SeasonEpisodeDTO> Episodes { get; set; } = new();

    }
}
