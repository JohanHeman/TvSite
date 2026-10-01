using System;
using System.Collections.Generic;
using System.Text;

namespace TvSite.Domain.Entities
{
    public class TvSeriesEpisode
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly AirDate { get; set; }
        public int EpisodeNumber { get; set; }
        public string ImagePath { get; set; }
    }
}
