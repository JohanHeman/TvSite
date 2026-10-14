// using Microsoft.Extensions.DependencyInjection;
// using TvSite.Infrastructure.Data;
//
// namespace TvSite.Tests.Integrationtests.Setup;
//
//create a classfixture for IntegrationTestFactory to create one factory and reuse it in tests

using TvSite.Tests.Integrationtests.Setup;

public class ApplicationTestBase<T>
{
//    // internal ApplicationDbContext applicationDbContext;
    //internal readonly IServiceScope scope;
    // public ApplicationTestBase(IntegrationTestFactory<T> factory)
    // {
    //     scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    //     applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //     applicationDbContext.Database.EnsureCreated();
    // }

    // public void Dispose()
    // {
    //     applicationDbContext.Database.EnsureDeleted();
    // }
}