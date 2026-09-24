using TvSite.Domain.Entities;
using TvSite.Domain.Enums;

namespace TvSite.Domain.Interfaces.Repositories;

public interface IMediaListEntryRepository
{
    public Task<List<MediaEntity>> GetMediaListByUserId(string userId, ListStateEnum.ListState listState);
  
    public Task CreateMediaEntry(MediaListEntry userMediaListEntry);
    public Task UpdateMediaEntryState(MediaListEntry userMediaListEntry);
}
