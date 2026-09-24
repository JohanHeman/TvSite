using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities;
using TvSite.Domain.Enums;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Infrastructure.Data;

namespace TvSite.Infrastructure.Repositories
{
    public class MediaListEntryRepository : IMediaListEntryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<MediaListEntry> _set;

        public MediaListEntryRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = dbContext.MediaListEntry;
        }

        public async Task CreateMediaListEntryAsync(MediaListEntry userMediaListEntry)
        {
            await _set.AddAsync(userMediaListEntry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<MediaListEntry>> GetMediaListByUserIdAsync(string userId, ListStateEnum.ListState listState)
        {
            return await _set
                .Where(entry  => entry.ApplicationUserId == userId)
                .Where(entry => entry.ListState == ((int)listState))
                .ToListAsync();
        }

        public async Task UpdateMediaListEntryStateAsync(MediaListEntry userMediaListEntry)
        {
            _set.Update(userMediaListEntry);
            await _dbContext.SaveChangesAsync();
        }
    }
}
