namespace TvSite.Domain.Entities.Api
{
    public class TvSeriesSeason
    {
        public string MediaId { get; set; }
        public string SeasonName { get; set; }
        public string Description { get; set; }
        public int SeasonNumber { get; set; }
        public string ImagePath { get; set; }
        public int EpisodeCount { get; set; }
        public DateOnly AirDate { get; set; }
        public List<TvSeriesEpisode> Episodes { get; set; } = new();
    }
}
