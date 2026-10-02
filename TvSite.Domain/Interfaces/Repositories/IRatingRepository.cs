using TvSite.Domain.Entities;

namespace TvSite.Domain.Interfaces.Repositories;

public interface IRatingRepository
{
    public Task<List<Rating>> GetAverageRating(string mediaId);
    public Task CreateRatingAsync(Rating rating);
    public Task DeleteRatingAsync(Rating rating);

}