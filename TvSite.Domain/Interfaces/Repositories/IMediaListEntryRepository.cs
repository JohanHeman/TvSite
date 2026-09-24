using TvSite.Domain.Entities;
using TvSite.Domain.Enums;

namespace TvSite.Domain.Interfaces.Repositories;

public interface IMediaListEntryRepository
{
    public Task<List<MediaListEntry>> GetMediaListByUserId(string userId, ListStateEnum.ListState listState);
    public Task CreateMediaListEntry(MediaListEntry userMediaListEntry);
    public Task UpdateMediaListEntryState(MediaListEntry userMediaListEntry);
}
