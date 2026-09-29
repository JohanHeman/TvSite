using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities;
using TvSite.Domain.Enums;
using TvSite.Domain.Interfaces.Services;
using TvSite.Infrastructure.Data;

namespace TvSite.Application.Service
{
    public class MediaListEntryService : IMediaListEntryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<MediaListEntry> _set;

        public MediaListEntryService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _set = dbContext.MediaListEntry;
        }

        public async Task CreateMediaListEntryAsync(MediaListEntry userMediaListEntry)
        {
            await _set.AddAsync(userMediaListEntry);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<MediaListEntry>> GetMediaListByUserIdAsync(Guid userId, ListStateEnum.ListState listState)
        {
            return await _set
                .Where(entry => entry.ApplicationUserId == userId)
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
