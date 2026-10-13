using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TvSite.Infrastructure.Data;

namespace TvSite.Tests.Integrationtests.Setup;

public class IntegrationTestFactory<T> : WebApplicationFactory<T> where T : class
{
    //configure startup for test project when running integrationtests
    private static readonly InMemoryDatabaseRoot DatabaseRoot = new();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting", DatabaseRoot);
            });
        });
    }
}
