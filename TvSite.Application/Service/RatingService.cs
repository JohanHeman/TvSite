using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities.Database;
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

        public async Task CreateRatingAsync(int stars, string episodeMediaId, Guid userId)
        {
            if (userId == Guid.Empty) return;

            if (string.IsNullOrWhiteSpace(episodeMediaId))
                throw new ArgumentException("Episode media id cannot be null or empty");

            Rating rating = new()
            {
                Id = Guid.NewGuid(),
                Stars = stars,
                ApplicationUserId = userId,
                EpisodeMediaId = episodeMediaId
            };
            await _repository.CreateRatingAsync(rating);
        }

        public async Task UpdateRatingAsync(Rating rating, int stars)
        {
            if (rating.Stars == stars) return;
            rating.Stars = stars;
            await _repository.UpdateRatingAsync(rating);
        }

        public async Task<Rating?> GetRatingByIdAsync(string episodeMediaId, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(episodeMediaId))
                throw new ArgumentException("Episode media id cannot be null or empty");
            var rating = await _repository.GetRatingByIdAsync(episodeMediaId, userId);
            return rating;
        }

        public async Task<double> GetAverageRatingByEpisodeMediaIdAsync(string mediaId)
        {
            var ratings = await _repository.GetAverageRating(mediaId);
            if (ratings.Count == 0) return 0;
            return ratings.Average(r => r.Stars);
        }
    }
}
