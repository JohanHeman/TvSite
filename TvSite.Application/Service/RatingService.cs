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

        public async Task CreateRatingAsync(Rating rating)
        {
            await _repository.CreateRatingAsync(rating);
        }

        public async Task DeleteRatingAsync(Rating rating)
        {
            await _repository.DeleteRatingAsync(rating);
        }

        public async Task<double> GetAverageRatingByMediaIdAsync(string mediaId)
        {
            var ratings = await _repository.GetAverageRating(mediaId);
            return ratings.Average(r => r.Stars);
        }
    }
}
