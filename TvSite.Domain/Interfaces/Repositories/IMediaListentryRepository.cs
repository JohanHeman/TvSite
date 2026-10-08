using TvSite.Domain.Entities;
using TvSite.Domain.Enums;

namespace TvSite.Domain.Interfaces.Repositories;

public interface IMediaListentryRepository
{
    public Task<List<MediaListEntry>> GetMediaListByUserIdAsync(Guid userId, ListStateEnum.ListState listState);
    public Task CreateMediaListEntryAsync(MediaListEntry userMediaListEntry);
    public Task UpdateMediaListEntryStateAsync(MediaListEntry userMediaListEntry);
    public Task<MediaListEntry?> GetMediaListEntryByIdAsync(Guid mediaListEntryId);
}