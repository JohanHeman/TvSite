using Moq;
using TvSite.Application.ServiceAPI;
using TvSite.Domain.Entities.Api;
using TvSite.Domain.InterfacesAPI.Repositories;

namespace TvSite.Tests.UnitTests;

public class ApiTests
{
    // using moq nuget package
    // sets up a fake version of IApiRepository
    private readonly Mock<IAPIRepository> _mockRepository;
    private readonly APIService _sut;
    public ApiTests()
    {
        _mockRepository = new Mock<IAPIRepository>();
        _sut = new APIService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetTvShowsSearchResultByTitle_ShouldReturn_ListOfTvShows_WhenValidSearch()
    {
        // Arrange
        var input = "breaking";
        var expected = "Breaking Bad";
        _mockRepository.Setup(r => r.GetTvShowsSearchResult(input))
            .ReturnsAsync(new List<SearchResult>()
            {
                new SearchResult()
                {
                    MediaId = "23",
                    Title = "Breaking Bad",
                }
            });

        // Act
        var actual = await _sut.GetTvShowsSearchResultByTitle(input);

        // Assert
        Assert.Equal(expected, actual[0].Title);
    }

    [Fact]
    public async Task GetTvShowsFromDiscover_ShouldReturn_EmptyList_WhenNoResults()
    {
        _mockRepository.Setup(r => r.GetTvShowDiscoverListAsync())
            .ReturnsAsync(new List<DisplayMedia>());

        var expected = new List<DisplayMedia>();

        var actual = await _sut.GetTvShowsFromDiscover();

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetTvShowDetailsById_ShouldReturn_TitleGameOfThrones_WhenMatchingId()
    {
        var tvShowId = "1399";
        var expectedTitle = "Game of Thrones";

        _mockRepository.Setup(repo => repo.GetTvShowDetails(tvShowId))
            .ReturnsAsync(
            new TvSeries()
            {
                Id = "1399",
                Name = "Game of Thrones"
            });

        var actual = await _sut.GetTvShowDetailsById(tvShowId);

        Assert.Equal(expectedTitle, actual.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public async Task GetTvShowDetailsById_ShouldReturn_Null_WhenNullOrEmptyOrWhiteSpace(string? tvShowId)
    {
        // Act
        var actual = await _sut.GetTvShowDetailsById(tvShowId!);

        // Asserts
        Assert.Equal(null!, actual);
        _mockRepository.Verify(repo => repo.GetTvShowDetails(tvShowId!), Times.Never);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public async Task GetSeasonDetails_ShouldReturn_Null_WhenNullOrEmptyOrWhiteSpace(string? tvShowId)
    {
        // Act
        var actual = await _sut.GetSeasonDetails(tvShowId!, 0);

        // Asserts
        Assert.Equal(null!, actual);
        _mockRepository.Verify(repo => repo.GetSeasonDetails(tvShowId!, 0), Times.Never);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public async Task GetEpisodeDetails_ShouldReturn_Null_WhenNullOrEmptyOrWhiteSpace(string? tvShowId)
    {
        // Act
        var actual = await _sut.GetEpisodeDetails(tvShowId!, 0, 0);

        // Asserts
        Assert.Equal(null!, actual);
        _mockRepository.Verify(repo => repo.GetEpisodeDetailsAsync(tvShowId!, 0, 0), Times.Never);
    }
}
