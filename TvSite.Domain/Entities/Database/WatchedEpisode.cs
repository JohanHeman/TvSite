namespace TvSite.Domain.Entities.Database
{
    public class WatchedEpisode
    {
        public Guid Id { get; set; }
        public string EpisodeMediaId { get; set; }
        public string TvSeriesId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
