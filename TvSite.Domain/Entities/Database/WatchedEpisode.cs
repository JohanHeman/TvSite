namespace TvSite.Domain.Entities.Database
{
    public class WatchedEpisode
    {
        public Guid Id { get; set; }
        public string EpisodeId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }

        public bool IsSoftDeleted { get; set; }
    }
}
