namespace TvSite.Domain.Entities.Display;

// Needed for displaying recently watched episodes
public class DisplayWatchedEpisode
{
    // From Db model WatchedEpisode
    public string TvSeriesId { get; set; }
    public int SeasonNumber { get; set; }
    public int EpisodeNumber { get; set; }
    public DateTime DateTimeWatched { get; set; }
    // From Api model TvSeasonEpisode
    public string Title { get; set; }
    public string ImagePath { get; set; }
}
