using TvSite.Domain.Entities.Api;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Enums;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;
using TvSite.Domain.InterfacesAPI.Services;
using static TvSite.Domain.Enums.ListStateEnum;

namespace TvSite.Application.Service
{
    public class MediaListEntryService : IMediaListEntryService
    {
        private readonly IMediaListentryRepository _repository;
        private readonly IAPIService _APIService;

        public MediaListEntryService(IMediaListentryRepository repository, IAPIService APIService)
        {
            _repository = repository;
            _APIService = APIService;
        }

        public async Task<List<MediaListEntry>> GetMediaListByUserIdAsync(Guid userId, ListStateEnum.ListState listState)
        {
            return await _repository.GetMediaListByUserIdAsync(userId, listState);
        }

        public async Task CreateMediaListEntryAsync(MediaListEntry userMediaListEntry)
        {
            // Get userList to see if the tvshow already is added
            var userMediaList = await _repository.
                GetMediaListByUserIdAsync
                (userMediaListEntry.ApplicationUserId, 
                (ListStateEnum.ListState)userMediaListEntry.ListState);
            
            var existingMediaListEntry = userMediaList.FirstOrDefault(entry => entry.MediaId == userMediaListEntry.MediaId);

            if (existingMediaListEntry != null) return;

            await _repository.CreateMediaListEntryAsync(userMediaListEntry);
        }

        public async Task<List<TvSeries>> GetTvShowsFromUserListAsync(Guid userId, ListStateEnum.ListState listState)
        {
            var tvShows = new List<TvSeries>();

            var mediaListEntries = await _repository.GetMediaListByUserIdAsync(userId, listState);

            foreach (var mediaListEntry in mediaListEntries)
            {
                var tvShow = await _APIService.GetTvShowDetailsById(mediaListEntry.MediaId);
                tvShows.Add(tvShow);
            }

            return tvShows;
        }

        public async Task DeleteMediaListEntryAsync(Guid userId, string mediaId, ListStateEnum.ListState listState)
        {
            var mediaList = await _repository.GetMediaListByUserIdAsync(userId, listState);

            var mediaListEntry = mediaList.FirstOrDefault(media => media.MediaId == mediaId);

            if (mediaListEntry == null) return;

            await _repository.DeleteMediaListEntryAsync(mediaListEntry);
        }

        /// <summary>
        /// Add new media entry or changes state from following to stopwatching or other way around
        /// </summary>
        public async Task AddOrUpdateMediaEntryList(Guid userId, string tvShowMediaId)
        {
            if (userId == Guid.Empty || string.IsNullOrWhiteSpace(tvShowMediaId)) return;

            // Get mediaListEnty from FollowList and StoppedWatchingList
            var followedMediaEntry = await _repository.GetMediaListEntryByIdAsync(userId, tvShowMediaId, ListStateEnum.ListState.Following);
            var stoppedWatchingMediaEntry = await _repository.GetMediaListEntryByIdAsync(userId, tvShowMediaId, ListStateEnum.ListState.StoppedWatching);

            // If TvShow exists in FollowingList - switch to StoppedWatchingList
            if (followedMediaEntry != null)
            {
                followedMediaEntry.ListState = (int)ListStateEnum.ListState.StoppedWatching;
                await _repository.UpdateMediaListEntryStateAsync(followedMediaEntry);
            }
            // If TvShow exists in StopWatchingList - switch to FollowingList
            else if(stoppedWatchingMediaEntry != null)
            {
                stoppedWatchingMediaEntry.ListState = (int)ListStateEnum.ListState.Following;
                await _repository.UpdateMediaListEntryStateAsync(stoppedWatchingMediaEntry);
            }
            // If TvShow is not in FollowingList or in StoppedWatchingList - Add to FollowingList
            else
            {
                var mediaListEntry = new MediaListEntry
                {
                    MediaId = tvShowMediaId,
                    ListState = (int)ListStateEnum.ListState.Following,
                    ApplicationUserId = userId
                };
                await _repository.CreateMediaListEntryAsync(mediaListEntry);
            }
        }
    }
}
