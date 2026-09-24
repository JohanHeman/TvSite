using TvSite.Domain.Entities;
using TvSite.Domain.Enums;

namespace TvSite.Domain.Interfaces.Repositories;

public interface IMediaEntryRepository
{
    public Task<List<MediaEntity>> GetMediaListByUserId(string userId, ListStateEnum.ListState listState);

    public Task CreateMediaEntry(MediaEntry userMediaEntry);
    public Task UpdateMediaEntryState(MediaEntry userMediaEntry);
}
