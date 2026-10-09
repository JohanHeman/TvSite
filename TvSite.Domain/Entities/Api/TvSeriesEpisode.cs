namespace TvSite.Domain.Entities.Api
{
    public class TvSeriesEpisode
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateOnly AirDate { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string ImagePath { get; set; }
    }
}
