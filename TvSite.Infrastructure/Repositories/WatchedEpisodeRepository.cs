using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Infrastructure.Data;

namespace TvSite.Infrastructure.Repositories;

public class WatchedEpisodeRepository : IWatchedEpisodeRepository
{


    private readonly ApplicationDbContext _context;
    private readonly DbSet<WatchedEpisode> _set;

    public WatchedEpisodeRepository(ApplicationDbContext context)
    {
        _context = context;
        _set = context.WatchedEpisodes;
    }


    public async Task CreateWatchedEpisode(WatchedEpisode episode)
    {
        await _set.AddAsync(episode);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteWatchedEpisode(WatchedEpisode episode)
    {
        _set.Remove(episode);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> IsWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId)
    {
        var episode = await _set
            .Where(episode => episode.EpisodeMediaId == episodeMediaId)
            .Where(episode => episode.UserId == userId)
            .SingleOrDefaultAsync();

        return episode != null ? true : false;
    }

    public async Task<WatchedEpisode?> GetWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId)
    {
        var episode = await _set
            .Where(episode => episode.EpisodeMediaId == episodeMediaId)
            .Where(episode => episode.UserId == userId)
            .SingleOrDefaultAsync();

        return episode;
    }



}
