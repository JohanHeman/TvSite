using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain.Entities;

namespace TvSite.Domain.Interfaces.Services
{
    public interface IRatingService
    {
        public Task<float> GetAverageRatingByMediaIdAsync(string mediaId);

        public Task CreateRatingAsync(Rating rating);
        public Task DeleteRatingAsync(Rating rating);
    }
}
