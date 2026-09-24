namespace TvSite.Domain.Entities;

public class MediaListEntry
{
    public string Id { get; set; }
    public string MediaId { get; set; }
    public Media Media { get; set; }
    public int ListState { get; set; }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}