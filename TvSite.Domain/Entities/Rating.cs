namespace TvSite.Domain.Entities;

public class Rating
{
    public string Id{ get; set; }

    private int _stars;
    public int Stars { get { return _stars; } set 
        {
            if (value < ApplicationSettings.StarsMin)
                throw new ArgumentException($"Stars cannot be below {ApplicationSettings.StarsMin}", "Stars");

            if (value > ApplicationSettings.StarsMax)
                throw new ArgumentException($"Stars cannot be above {ApplicationSettings.StarsMax}", "Stars");

            _stars = value;
        }
    }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser{ get; set; }
    public string MediaId{ get; set; }
    public Media Media{ get; set; }
}