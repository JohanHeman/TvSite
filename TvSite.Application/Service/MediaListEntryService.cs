using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities;
using TvSite.Domain.Enums;
using TvSite.Domain.Interfaces.Services;
using TvSite.Domain.InterfacesAPI.Services;
using TvSite.Infrastructure.Data;

namespace TvSite.Application.Service
{
    public class MediaListEntryService : IMediaListEntryService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<MediaListEntry> _set;
        private readonly IAPIService _APIService;

        public MediaListEntryService(ApplicationDbContext dbContext, IAPIService APIService)
        {
            _dbContext = dbContext;
            _set = dbContext.MediaListEntries;
            _APIService = APIService;
        }

        public async Task CreateMediaListEntryAsync(MediaListEntry userMediaListEntry)
        {
            var existingMediaEntry = await _set.FirstOrDefaultAsync(existingMediaEntry =>
                existingMediaEntry.ApplicationUserId == userMediaListEntry.ApplicationUserId &&
                existingMediaEntry.MediaId == userMediaListEntry.MediaId &&
                existingMediaEntry.ListState == userMediaListEntry.ListState);

            if (existingMediaEntry != null) return;

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

        public async Task<List<TvSeries>> GetTvShowsFromUserListAsync(Guid userId, ListStateEnum.ListState listState)
        {
            var tvShows = new List<TvSeries>();

            var mediaListEntries = await GetMediaListByUserIdAsync(userId, listState);

            foreach (var mediaListEntry in mediaListEntries)
            {
                var tvShow = await _APIService.GetTvShowDetailsById(mediaListEntry.MediaId);
                tvShows.Add(tvShow);
            }

            return tvShows;
        }
    }
}
