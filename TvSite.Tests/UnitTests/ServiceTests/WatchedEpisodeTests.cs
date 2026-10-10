using Moq;
using TvSite.Application.Service;
using TvSite.Domain.Entities.Database;
using TvSite.Domain.Interfaces.Repositories;
using TvSite.Domain.Interfaces.Services;
using TvSite.Domain.InterfacesAPI.Services;

namespace TvSite.Tests.UnitTests.ServiceTests;

public class WatchedEpisodeTests
{
    private readonly IWatchedEpisodeService _sut;

    // Mock dependencies to setup the _sut
    private readonly Mock<IWatchedEpisodeRepository> _mockRepo;
    private readonly Mock<IAPIService> _apiService;
    public WatchedEpisodeTests()
    {
        _mockRepo = new Mock<IWatchedEpisodeRepository>();
        _apiService = new Mock<IAPIService>();

        _sut = new WatchedEpisodeService(_mockRepo.Object, _apiService.Object);
    }

    [Theory]
    [MemberData(nameof(InvalidIds))]
    public async Task IsWatchedEpisodeByUserAsync_ReturnsFalse_WhenIdsAreInvalid(string? episodeMediaId, Guid userId)
    {
        // Arrange
        bool expected = false;

        // Act
        var act = await _sut.IsWatchedEpisodeByUserAsync(episodeMediaId!, userId);

        // Assert
        Assert.False(act);
    }

    [Theory]
    [MemberData(nameof(CreateWatchedEpisodeInvalidInputs))]
    public async Task CreateWatchedEpisode_DoesNotCallRepo_WhenIdsAreInvalid(string episodeMediaId, Guid userId, string tvSeriesId, int seasonNumber, int episodeNumber)
    {
        // Act, Arange
        // Expecting early exit
        await _sut.CreateWatchedEpisode(episodeMediaId, userId, tvSeriesId, seasonNumber, episodeNumber);

        // Assert
        // Ensure repo was never called
        _mockRepo.Verify(repo => repo.CreateWatchedEpisode(It.IsAny<WatchedEpisode>()), Times.Never);
    }

    // Need memberdata due to Guids. Guids cant be used in Inlinedata due requireing runtime to compile the value.
    // Member data
    public static IEnumerable<Object[]> InvalidIds => 
    [
        ["", Guid.Empty],
        ["   ", Guid.Empty],
        [null!, Guid.Empty],
        ["", Guid.NewGuid()],
        ["   ", Guid.NewGuid()],
        [null!, Guid.NewGuid()],
        ["Abc", Guid.Empty],
        ["Abc", Guid.NewGuid()]
    ];

    // Cant use inline data for guid as it set / gets it on runtime.
    public static IEnumerable<Object[]> CreateWatchedEpisodeInvalidInputs =>
    [
        ["episodeMediaId", Guid.NewGuid(), "", 0, 0],
        ["episodeMediaId", Guid.NewGuid(), "  ", 0, 0],
        [null!, Guid.NewGuid(), "seriesId", 0, 0],
        ["episodeMediaId", Guid.Empty, "seriesId", 0, 0],
        ["   ", Guid.NewGuid(), "", 0, 0],
        [null!, Guid.NewGuid(), null!, 0, 0],
        ["", Guid.Empty, "", 0, 0],
    ];
}

