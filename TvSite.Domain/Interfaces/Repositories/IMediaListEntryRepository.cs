using TvSite.Domain.Entities;
using TvSite.Domain.Enums;

namespace TvSite.Domain.Interfaces.Repositories;

public interface IMediaListEntryRepository
{
    public Task<List<MediaListEntry>> GetMediaListByUserIdAsync(string userId, ListStateEnum.ListState listState);
    public Task CreateMediaListEntryAsync(MediaListEntry userMediaListEntry);
    public Task UpdateMediaListEntryStateAsync(MediaListEntry userMediaListEntry);
}
