using TvSite.Domain.Entities;

namespace TvSite.Domain.Interfaces.Repositories;

public interface IRatingRepositroy
{
    public Task<Rating> GetAvrageRatingByMediaIdAsync(string mediaId);

    public Task CreateRatingAsync(Rating rating);
    public Task DeleteRatingAsync(Rating rating);

}
