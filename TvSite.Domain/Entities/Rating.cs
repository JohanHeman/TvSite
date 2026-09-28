namespace TvSite.Domain.Entities;

public class Rating
{
    public string Id{ get; set; }

    private int _stars;
    public int Stars { get { return _stars; } set 
        {
            if (value < _minStars)
                throw new ArgumentException($"Stars cannot be below {_minStars}", "Stars");

            if (value > _maxStars)
                throw new ArgumentException($"Stars cannot be above {_maxStars}", "Stars");

            _stars = value;
        }
    }
    public string ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser{ get; set; }
    public string MediaId{ get; set; }
    public Media Media{ get; set; }


    // Stars constraints
    private int _minStars = 0;
    private int _maxStars = 10;
     
}