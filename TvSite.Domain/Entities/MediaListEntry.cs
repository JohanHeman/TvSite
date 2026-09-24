namespace TvSite.Domain.Entities;

public class MediaListEntry
{
    public string Id { get; set; }
    public MediaEntity Media { get; set; }
    public int ListState { get; set; }
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}