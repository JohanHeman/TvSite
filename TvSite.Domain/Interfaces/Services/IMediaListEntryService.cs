using System;
using System.Collections.Generic;
using System.Text;
using TvSite.Domain.Entities;
using TvSite.Domain.Enums;

namespace TvSite.Domain.Interfaces.Services
{
    public interface IMediaListEntryService
    {
        public Task<List<MediaListEntry>> GetMediaListByUserIdAsync(Guid userId, ListStateEnum.ListState listState);

        public Task CreateMediaListEntryAsync(MediaListEntry userMediaListEntry);
        public Task UpdateMediaListEntryStateAsync(MediaListEntry userMediaListEntry);
    }
}
