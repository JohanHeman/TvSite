using Microsoft.EntityFrameworkCore;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Entities.Display;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;
using TvSite.Domain.InterfacesAPI.Services;


namespace TvSite.Application.Service;

public class WatchedEpisodeService : IWatchedEpisodeService
{
    private readonly IWatchedEpisodeRepository _repository;
    private readonly IAPIService _APIService;

    public WatchedEpisodeService(IWatchedEpisodeRepository repository, IAPIService apiService)
    {
        _repository = repository;
        _APIService = apiService;
    }

    public async Task<bool> IsWatchedEpisodeByUserAsync(string episodeMediaId, Guid userId)
    {
        if (userId == Guid.Empty || string.IsNullOrWhiteSpace(episodeMediaId)) return false;

        try
        {
            return await _repository.IsWatchedEpisodeByUserAsync(episodeMediaId, userId);
        }
        catch (Exception ex)
        {
            throw new DbUpdateException("Could not get WatchedEpisode \nInner Exception: " + ex.InnerException);
        }
    }

    public async Task CreateWatchedEpisode(string episodeMediaId, Guid userId, string tvSeriesId, int seasonNumber, int episodeNumber)
    {
        if (userId == Guid.Empty || string.IsNullOrWhiteSpace(episodeMediaId) || string.IsNullOrWhiteSpace(tvSeriesId)) return;

        var watchedEpisode = new WatchedEpisode()
        {
            Id = Guid.NewGuid(),
            EpisodeMediaId = episodeMediaId,
            TvSeriesId = tvSeriesId,
            SeasonNumber = seasonNumber,
            EpisodeNumber = episodeNumber,
            UserId = userId,
            DateTime = DateTime.Now,
        };

        try
        {
            await _repository.CreateWatchedEpisode(watchedEpisode);
        }
        catch
        {
            throw new DbUpdateException("Could not create WatchedEpisode");
        }
    }

    public async Task DeleteWatchedEpisode(string episodeMediaId, Guid userId)
    {
        if (userId == Guid.Empty || string.IsNullOrWhiteSpace(episodeMediaId)) return;

        try
        {
            WatchedEpisode? watchedEpisode;
            watchedEpisode = await _repository.GetWatchedEpisodeByUserAsync(episodeMediaId, userId);

            if (watchedEpisode == null) return;

            await _repository.DeleteWatchedEpisode(watchedEpisode);
        }
        catch
        {
            throw new DbUpdateException("Could not delete WatchedEpisode");
        }
    }

    public async Task CreateOrDeleteWatchedEpisode(string episodeMediaId, Guid userId, string tvSeriesId, int seasonNumber, int episodeNumber)
    {
        if (userId == Guid.Empty || string.IsNullOrWhiteSpace(episodeMediaId)) return;

        bool isExistingWatchedEpisode = await IsWatchedEpisodeByUserAsync(episodeMediaId, userId);

        if (!isExistingWatchedEpisode)
            await CreateWatchedEpisode(episodeMediaId, userId, tvSeriesId, seasonNumber, episodeNumber);

        else
            await DeleteWatchedEpisode(episodeMediaId, userId);
    }

    public async Task<List<DisplayWatchedEpisode>> GetDisplayWatchedEpisodesByUserIdAsync(Guid userId)
    {
        var tvSeriesEpisodes = new List<DisplayWatchedEpisode>();

        var watchedEpisodes = await _repository.GetWatchedEpisodesByUserIdAsync(userId);
        foreach (var episode in watchedEpisodes)
        {
            var tvSeriesEpisode = await _APIService.GetEpisodeDetails(episode.TvSeriesId, episode.SeasonNumber, episode.EpisodeNumber);

            if (tvSeriesEpisode != null)
            {
                var displayEntity = new DisplayWatchedEpisode()
                {
                    TvSeriesId = episode.TvSeriesId,
                    SeasonNumber = episode.SeasonNumber,
                    EpisodeNumber = episode.EpisodeNumber,
                    DateTimeWatched = episode.DateTime,

                    Title = tvSeriesEpisode.Title,
                    ImagePath = tvSeriesEpisode.ImagePath,

                };

                tvSeriesEpisodes.Add(displayEntity);
            }

        }

        return tvSeriesEpisodes;
    }
}
