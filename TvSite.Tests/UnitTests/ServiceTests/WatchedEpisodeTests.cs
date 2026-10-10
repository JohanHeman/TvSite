using Microsoft.EntityFrameworkCore;
using Moq;
using TvSite.Application.Service;
using TvSite.Domain.Entities.Api;
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
    [MemberData(nameof(IsWatchedEpisodeByUserData))]
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
    [MemberData(nameof(CreateWatchedEpisodeInvalidInputsData))]
    public async Task CreateWatchedEpisode_DoesNotCallRepo_WhenIdsAreInvalid(string episodeMediaId, Guid userId, string tvSeriesId, int seasonNumber, int episodeNumber)
    {
        // Act Arange
        // Expecting early exit
        await _sut.CreateWatchedEpisode(episodeMediaId, userId, tvSeriesId, seasonNumber, episodeNumber);

        // Assert
        // Ensure repo was never called
        _mockRepo.Verify(repo => repo.CreateWatchedEpisode(It.IsAny<WatchedEpisode>()), Times.Never);
    }

    [Theory]
    [MemberData(nameof(CreateWatchedEpisodeValidInputsData))]
    public async Task CreateWatchedEpisode_CallsRepoOnce_WhenInputsAreValid(string episodeMediaId, Guid userId, string tvSeriesId, int seasonNumber, int episodeNumber)
    {
        // Arrange Act
        await _sut.CreateWatchedEpisode(episodeMediaId, userId, tvSeriesId, seasonNumber, episodeNumber);

        // Assert
        _mockRepo.Verify(repo => repo.CreateWatchedEpisode(It.IsAny<WatchedEpisode>()), Times.Once);
    }

    [Theory]
    [MemberData(nameof(DeleteWatchedEpisodeInvalidData))]
    public async Task DeleteWatchedEpisode_NeverCallsRepo_WhenInvalidIds(string episodeMediaId, Guid userId)
    {
        // Act Arange
        // Expecting early exit
        await _sut.DeleteWatchedEpisode(episodeMediaId, userId);

        // Assert
        // Ensure repo was never called
        _mockRepo.Verify(repo => repo.DeleteWatchedEpisode(It.IsAny<WatchedEpisode>()), Times.Never);
    }

    [Fact]
    public async Task DeleteWatchedEpisode_CallsRepoOnce_WhenValidInput()
    {
        // Arrange
        string episodeMediaId = "validId";
        var userId = Guid.NewGuid();
        var watchedEpisode = new WatchedEpisode()
        {
            EpisodeMediaId = episodeMediaId,
            UserId = userId,
        };

        // Setup repo methods called in _sut method
        _mockRepo.Setup(repo => repo.GetWatchedEpisodeByUserAsync(episodeMediaId, userId)).ReturnsAsync(watchedEpisode);

        // Act
        await _sut.DeleteWatchedEpisode(episodeMediaId, userId);

        // Assert
        _mockRepo.Verify(repo => repo.DeleteWatchedEpisode(watchedEpisode), Times.Once);
    }

    // Need memberdata due to Guids. 
    // Cant use inline data for guid as it need complie time
    // Member data
    public static IEnumerable<Object[]> IsWatchedEpisodeByUserData => 
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

    public static IEnumerable<Object[]> CreateWatchedEpisodeInvalidInputsData =>
    [
        ["episodeMediaId", Guid.NewGuid(), "", 0, 0],
        ["episodeMediaId", Guid.NewGuid(), "  ", 0, 0],
        [null!, Guid.NewGuid(), "seriesId", 0, 0],
        ["episodeMediaId", Guid.Empty, "seriesId", 0, 0],
        ["   ", Guid.NewGuid(), "", 0, 0],
        [null!, Guid.NewGuid(), null!, 0, 0],
        ["", Guid.Empty, "", 0, 0],
    ];

    public static IEnumerable<Object[]> CreateWatchedEpisodeValidInputsData =>
    [
        ["episodeMediaId", Guid.NewGuid(), "valid", 1, 1],
        ["episodeMediaId", Guid.NewGuid(), "t", 1, 2],
        ["tisissparta", Guid.NewGuid(), "test", 2, 1],
    ];

    public static IEnumerable<Object[]> DeleteWatchedEpisodeInvalidData =>
    [
        [null!, Guid.Empty],
        ["", Guid.Empty],
        ["  ", Guid.Empty],
        ["id", Guid.Empty],
        [null!, Guid.Empty],
        ["  ", Guid.NewGuid()],
        ["", Guid.NewGuid()],
        [null!, Guid.NewGuid()],
    ];
}

