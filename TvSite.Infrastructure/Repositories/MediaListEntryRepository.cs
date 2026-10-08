using TvSite.Domain.Entities;
using TvSite.Domain.Enums;
using TvSite.Domain.Interfaces.Repositories;

namespace TvSite.Infrastructure.Repositories;

public class MediaListEntryRepository : IMediaListentryRepository
{
    public Task<List<MediaListEntry>> GetMediaListByUserIdAsync(Guid userId, ListStateEnum.ListState listState)
    {
        throw new NotImplementedException();
    }

    public Task CreateMediaListEntryAsync(MediaListEntry userMediaListEntry)
    {
        throw new NotImplementedException();
    }

    public Task UpdateMediaListEntryStateAsync(MediaListEntry userMediaListEntry)
    {
        throw new NotImplementedException();
    }

    public Task<List<TvSeries>> GetTvShowsFromUserListAsync(Guid userId, ListStateEnum.ListState listState)
    {
        throw new NotImplementedException();
    }
}