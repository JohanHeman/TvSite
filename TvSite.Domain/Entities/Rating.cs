namespace TvSite.Domain.Entities;

public class Rating
{
    public string Id{ get; set; }
    public int Stars { get; set; }
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser{ get; set; }
    public string MediaId{ get; set; }
    // Media media 
     
}