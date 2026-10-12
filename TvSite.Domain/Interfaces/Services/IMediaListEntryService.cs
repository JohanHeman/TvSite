using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain.Entities.Api;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Enums;

namespace TvSite.Domain.Interfaces.Services
{
    public interface IMediaListEntryService
    {
        public Task<List<MediaListEntry>> GetMediaListByUserIdAsync(Guid userId, ListStateEnum.ListState listState);
        public Task CreateMediaListEntryAsync(MediaListEntry userMediaListEntry);
        public Task<List<TvSeries>> GetTvShowsFromUserListAsync(Guid userId, ListStateEnum.ListState listState);
        public Task DeleteMediaListEntryAsync(Guid userId, string mediaId, ListStateEnum.ListState listState);
        public Task AddOrUpdateMediaEntryList(Guid userId, string mediaId);
    }
}
