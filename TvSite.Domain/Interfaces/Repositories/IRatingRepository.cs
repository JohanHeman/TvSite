using TvSite.Domain.Entities.Database;

namespace TvSite.Domain.Interfaces.Repositories;

public interface IRatingRepository
{
    public Task<List<Rating>> GetAverageRating(string mediaId);
    public Task CreateRatingAsync(Rating rating);
    public Task DeleteRatingAsync(Rating rating);
    public Task UpdateRatingAsync(Rating rating);
    public Task<Rating?> GetRatingByIdAsync(string mediaId, Guid userId);

}
