namespace TvSite.Domain.Entities;

public class Comment
{
    public string Id { get; set; }
    public string Text { get; set; }
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser{ get; set; }
    public string MediaId{ get; set; }
    public MediaEntity MediaEntity { get; set; }
}