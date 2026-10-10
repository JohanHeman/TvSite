using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Enums;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Infrastructure.Data;

namespace TvSite.Infrastructure.Repositories;

public class MediaListEntryRepository : IMediaListentryRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly DbSet<MediaListEntry> _set;

    public MediaListEntryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _set = dbContext.MediaListEntries;
    }

    public async Task<List<MediaListEntry>> GetMediaListByUserIdAsync(Guid userId, ListStateEnum.ListState listState)
    {
        return await _set
            .Where(entry => entry.ApplicationUserId == userId)
            .Where(entry => entry.ListState == ((int)listState))
            .ToListAsync();
    }

    public async Task CreateMediaListEntryAsync(MediaListEntry userMediaListEntry)
    {
        await _set.AddAsync(userMediaListEntry);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateMediaListEntryStateAsync(MediaListEntry userMediaListEntry)
    {
        _set.Update(userMediaListEntry);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<MediaListEntry?> GetMediaListEntryByIdAsync(Guid mediaListEntryId)
    {
        return await _set.FirstOrDefaultAsync(mediaListEntry => mediaListEntry.Id == mediaListEntryId);
    }

    public async Task DeleteMediaListEntryAsync(MediaListEntry userMediaListEntry)
    {
        _dbContext.MediaListEntries.Remove(userMediaListEntry);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<MediaListEntry?> GetMediaListEntryByIdAsync(Guid userId, string tvShowId, ListStateEnum.ListState listState)
    {
        return await _set
            .Where(entry => entry.ApplicationUserId == userId)
            .Where(entry => entry.MediaId == tvShowId)
            .Where(entry => entry.ListState == ((int)listState))
            .SingleOrDefaultAsync();
    }
}
