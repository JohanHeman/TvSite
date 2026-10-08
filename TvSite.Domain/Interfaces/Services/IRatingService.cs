using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain.Entities.Database;

namespace TvSite.Domain.Interfaces.Services
{
    public interface IRatingService
    {
        public Task<Double> GetAverageRatingByEpisodeMediaIdAsync(string episodeMediaId);
        public Task CreateRatingAsync(int stars, string episodeMediaId, Guid userId);
        public Task DeleteRatingAsync(Rating rating);
        public Task UpdateRatingAsync(Rating rating);
        public Task<Rating?> GetRatingByIdAsync(string episodeMediaId, Guid userId);
    }
}
