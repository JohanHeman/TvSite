using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain.Entities.Database;

namespace TvSite.Domain.Interfaces.Services
{
    public interface IRatingService
    {
        public Task<Double> GetAverageRatingByMediaIdAsync(string mediaId);
        public Task CreateRatingAsync(int stars, string mediaId, Guid userId);
        public Task DeleteRatingAsync(Rating rating);
        public Task UpdateRatingAsync(Rating rating);
        public Task<Rating?> GetRatingByIdAsync(string mediaId, Guid userId);
    }
}
