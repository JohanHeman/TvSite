using Moq;
using TvSite.Application.ServiceAPI;
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
    
    
    
}