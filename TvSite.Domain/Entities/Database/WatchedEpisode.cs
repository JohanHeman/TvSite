namespace TvSite.Domain.Entities.Database
{
    public class WatchedEpisode
    {
        public Guid Id { get; set; }
        public string EpisodeMediaId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
