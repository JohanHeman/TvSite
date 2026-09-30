using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TvSite.Infrastructure.DTO
{

    // Image path https://image.tmdb.org/t/p/w500//2RMejaT793U9KRk2IEbFfteQntE.jpg
    // Image path https://image.tmdb.org/t/p/{size}//{path}
    public class TvShowDetailsDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Title { get; set; }

        [JsonPropertyName("overview")]
        public string Description { get; set; }

        [JsonPropertyName("poster_path")]
        public string ImagePath { get; set; }

        [JsonPropertyName("seasons")]
        public TvShowDetailsSeasonDTO[] Seasons { get; set; }

        [JsonPropertyName("created_by")]
        public TvShowDetailsDirectorDTO[] Directors { get; set; }

        [JsonPropertyName("first_air_date")]
        public DateOnly AirDate { get; set; }
    }

    public class TvShowDetailsSeasonDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("season_number")]
        public int SeasonNumber { get; set; }

        [JsonPropertyName("name")]
        public string Title { get; set; }

        [JsonPropertyName("episode_count")]
        public int EpisodeCount { get; set; }

        [JsonPropertyName("poster_path")]
        public string ImagePath { get; set; }

        [JsonPropertyName("overview")]
        public string Description { get; set; }

        [JsonPropertyName("air_date")]
        public DateOnly AirDate { get; set; }
    }

    public class TvShowDetailsDirectorDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("profile_path")]
        public string ImagePath { get; set; }


    }
}
