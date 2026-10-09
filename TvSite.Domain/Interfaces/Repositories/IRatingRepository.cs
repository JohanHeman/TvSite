using TvSite.Domain.Entities.Database;

namespace TvSite.Domain.Interfaces.Repositories;

public interface IRatingRepository
{
    public Task<List<Rating>> GetAverageRating(string episodeMediaId);
    public Task CreateRatingAsync(Rating rating);
    public Task UpdateRatingAsync(Rating rating);
    public Task<Rating?> GetRatingByIdAsync(string episodeMediaId, Guid userId);
    public Task<Rating?> GetRatingByRatingIdAsync(Guid ratingId);

}
