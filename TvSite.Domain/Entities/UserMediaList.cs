namespace TvSite.Domain.Entities;

public class UserMediaList
{
    public List<string> Medias { get; set; } // change string to media later
    public int ListState { get; set; }
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
}