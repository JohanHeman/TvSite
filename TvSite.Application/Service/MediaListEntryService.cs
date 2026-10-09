using TvSite.Domain.Entities.Api;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Enums;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;
using TvSite.Domain.InterfacesAPI.Services;

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
            var existingMediaListEntry = await _repository.GetMediaListEntryByIdAsync(userMediaListEntry.Id);

            if (existingMediaListEntry != null) return;

            await _repository.CreateMediaListEntryAsync(userMediaListEntry);
        }

        public async Task UpdateMediaListEntryStateAsync(Guid userId, string mediaId, ListStateEnum.ListState listState)
        {
            var followingList = await _repository.GetMediaListByUserIdAsync(userId, ListStateEnum.ListState.Following);

            var userMediaListEntry = followingList.FirstOrDefault(media => media.MediaId == mediaId);

            if (userMediaListEntry == null) return;

            userMediaListEntry.ListState = (int)listState;

            await _repository.UpdateMediaListEntryStateAsync(userMediaListEntry);
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

        public async Task DeleteMediaListEntryAsync(Guid userId, string mediaId)
        {
            var followingList = await _repository.GetMediaListByUserIdAsync(userId, ListStateEnum.ListState.Following);
            
            var mediaListEntry = followingList.FirstOrDefault(media => media.MediaId == mediaId);

            if(mediaListEntry == null) return;

            await _repository.DeleteMediaListEntryAsync(mediaListEntry);
        }
    }
}
