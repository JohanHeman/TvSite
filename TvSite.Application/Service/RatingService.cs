using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;
using TvSite.Infrastructure.Data;
using TvSite.Infrastructure.Repositories;

namespace TvSite.Application.Service
{
    public class RatingService : IRatingService
    {
        private readonly IRatingRepository _repository;

        public RatingService(IRatingRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateRatingAsync(int stars, string mediaId, Guid userId)
        {
            Rating rating = new()
            {
                Id = Guid.NewGuid(),
                Stars = stars,
                ApplicationUserId = userId,
                MediaId = mediaId
            };
            await _repository.CreateRatingAsync(rating);
        }

        public async Task DeleteRatingAsync(Rating rating)
        {
            await _repository.DeleteRatingAsync(rating);
        }

        public async Task UpdateRatingAsync(Rating rating)
        {

            await _repository.UpdateRatingAsync(rating);
        }

        public Task<Rating?> GetRatingByIdAsync(string mediaId, Guid auserId)
        {
            var rating = _repository.GetRatingByIdAsync(mediaId, auserId);
            if (rating == null) throw new KeyNotFoundException();
            return rating;
        }

        public async Task<double> GetAverageRatingByMediaIdAsync(string mediaId)
        {
            var ratings = await _repository.GetAverageRating(mediaId);
            if (ratings.Count == 0) return 0;
            return ratings.Average(r => r.Stars);
        }
    }
}
