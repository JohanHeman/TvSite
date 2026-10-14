using Microsoft.EntityFrameworkCore;
using TvSite.Application.Interfaces.Services;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Entities.Display;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Application.Interfaces.Services;
using TvSite.Application.Interfaces.ApiService;


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

    // Checks if the user has seen the episode in question
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

        // Setup model
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
            // Save to db
            await _repository.CreateWatchedEpisode(watchedEpisode);
        }
        catch
        {
            throw new DbUpdateException("Could not create WatchedEpisode");
        }
    }

    // Delete
    public async Task DeleteWatchedEpisode(string episodeMediaId, Guid userId)
    {
        if (userId == Guid.Empty || string.IsNullOrWhiteSpace(episodeMediaId)) return;

        try
        {
            // Check if the item actual exist in db
            WatchedEpisode? watchedEpisode;
            watchedEpisode = await _repository.GetWatchedEpisodeByUserAsync(episodeMediaId, userId);

            if (watchedEpisode == null) return;

            // Remove if does exist
            await _repository.DeleteWatchedEpisode(watchedEpisode);
        }
        catch
        {
            throw new DbUpdateException("Could not delete WatchedEpisode");
        }
    }

    // Creates or delete Watched episode, depending if the user already has marked it as watched
    public async Task CreateOrDeleteWatchedEpisode(string episodeMediaId, Guid userId, string tvSeriesId, int seasonNumber, int episodeNumber)
    {
        if (userId == Guid.Empty || string.IsNullOrWhiteSpace(episodeMediaId)) return;

        // Check if user already has already marked it as seen
        bool isExistingWatchedEpisode = await IsWatchedEpisodeByUserAsync(episodeMediaId, userId);

        // Create
        if (!isExistingWatchedEpisode)
            await CreateWatchedEpisode(episodeMediaId, userId, tvSeriesId, seasonNumber, episodeNumber);
        // Delete
        else
            await DeleteWatchedEpisode(episodeMediaId, userId);
    }

    // Gets WatchedEpisodes with the combined db and Api model (DisplayWatchedEpisode)
    public async Task<List<DisplayWatchedEpisode>> GetDisplayWatchedEpisodesByUserIdAsync(Guid userId)
    {
        var tvSeriesEpisodes = new List<DisplayWatchedEpisode>();

        // Get all watched episodes from the user
        var watchedEpisodes = await _repository.GetWatchedEpisodesByUserIdAsync(userId);
        foreach (var episode in watchedEpisodes)
        {
            // Get episode from db
            var tvSeriesEpisode = await _APIService.GetEpisodeDetails(episode.TvSeriesId, episode.SeasonNumber, episode.EpisodeNumber);

            if (tvSeriesEpisode != null)
            {
                // Map db model and api model to new Display object
                var displayEntity = new DisplayWatchedEpisode()
                {
                    // from Db
                    TvSeriesId = episode.TvSeriesId,
                    SeasonNumber = episode.SeasonNumber,
                    EpisodeNumber = episode.EpisodeNumber,
                    DateTimeWatched = episode.DateTime,

                    // from api
                    Title = tvSeriesEpisode.Title,
                    ImagePath = tvSeriesEpisode.ImagePath,

                };

                tvSeriesEpisodes.Add(displayEntity);
            }
        }
        return tvSeriesEpisodes;
    }
}
