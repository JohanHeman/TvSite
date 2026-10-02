namespace TvSite.Domain.Entities;

public class Rating
{
    public Guid Id { get; set; }

    private int _stars;
    public int Stars
    {
        get { return _stars; }
        set
        {
            if (value < ApplicationSettings.StarsMin)
                throw new ArgumentException($"Stars cannot be below {ApplicationSettings.StarsMin}");

            if (value > ApplicationSettings.StarsMax)
                throw new ArgumentException($"Stars cannot be above {ApplicationSettings.StarsMax}");

            _stars = value;
        }
    }
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; }
    public string MediaId { get; set; }
}