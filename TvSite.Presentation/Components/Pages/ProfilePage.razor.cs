using TvSite.Application.ServiceAPI;
using TvSite.Domain.Entities.Api;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Enums;
using TvSite.Domain.Entities.Display;

namespace TvSite.Presentation.Components.Pages
{
    public partial class ProfilePage
    {
        private string DisplayName;
        private List<TvSeries> _followList = new();
        private List<TvSeries> _watchLaterList = new();
        private List<TvSeries> _stoppedWatchingList = new();
        private List<DisplayWatchedEpisode> _watchedEpisodes = new();

        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(authenticationState.User);

            if (user != null)
            {
                DisplayName = user.DisplayName;

                _followList = await MediaListEntryService.GetTvShowsFromUserListAsync(user.Id, ListStateEnum.ListState.Following);
                _watchLaterList = await MediaListEntryService.GetTvShowsFromUserListAsync(user.Id, ListStateEnum.ListState.ToBeWatched);
                _stoppedWatchingList = await MediaListEntryService.GetTvShowsFromUserListAsync(user.Id, ListStateEnum.ListState.StoppedWatching);

                _watchedEpisodes = await WatchedEpisodeService.GetWatchedEpisodesByUserIdAsync(user.Id);
            }
        }
    }
}
