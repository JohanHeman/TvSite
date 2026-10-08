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
    public async Task GetTvShowsSearchResult_ShouldReturn_ListOfTvShows_WhenValidSearch()
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
        var actual = await _sut.GetMediasByTitle(input);

        // Assert
        Assert.Equal(expected, actual[0].Title);
    }

    [Fact]
    public async Task GetTvShowsFromDiscover_ShouldReturn_EmptyList_WhenNoResults()
    {
        var input = "";
        _mockRepository.Setup(r => r.GetTvShowsSearchResult(input))
            .ReturnsAsync(new List<SearchResult>());

        var expected = new List<SearchResult>();

        var actual = await _sut.GetMediasByTitle(input);

        Assert.Equal(expected, actual);
    }

    [Fact]
    public async Task GetTvSeriesDetails_ShouldReturn_TitleGameOfThrones_WhenMatchingId()
    {
        var input = "1399";
        var expectedTitle = "Game of Thrones";

        _mockRepository.Setup(repo => repo.GetTvShowDetails(input))
            .ReturnsAsync(
            new TvSeries()
            {
                Id = "1399",
                Name = "Game of Thrones"
            });

        var actual = await _sut.GetTvShowDetailsById(input);

        Assert.Equal(expectedTitle, actual.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData(null)]
    public async Task GetTvSeriesDetails_ShouldReturn_Null_WhenNullOrEmptyOrWhiteSpace(string? input)
    {
        // Act
        var actual = await _sut.GetTvShowDetailsById(input!);

        // Asserts
        Assert.Equal(null!, actual);
        _mockRepository.Verify(repo => repo.GetTvShowDetails(input!), Times.Never);
    }
}
