using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Infrastructure.Data;

namespace TvSite.Infrastructure.Repositories
{
    public class RatingRepository : IRatingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Rating> _set;

        public RatingRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = dbContext.Ratings;
        }

        public async Task CreateRatingAsync(Rating rating)
        {
            await _set.AddAsync(rating);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteRatingAsync(Rating rating)
        {
            _set.Remove(rating);
            await _dbContext.SaveChangesAsync();
        }

        public Task<float> GetAverageRatingByMediaIdAsync(string mediaId)
        {
            throw new NotImplementedException();
        }
    }
}
