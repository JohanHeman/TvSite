namespace TvSite.Infrastructure.DTO;

public class MediaDTO
{
    
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string[] Directors { get; set; }
    public string[] Actors { get; set; }
    public DateOnly AirDate { get; set; }
    public string MediaImage { get; set; }
}