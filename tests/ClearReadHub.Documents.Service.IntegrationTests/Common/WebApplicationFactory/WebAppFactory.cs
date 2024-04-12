using ClearRead.Documents.Service.IntegrationTests.Common.WebApplicationFactory;

using ClearReadHub.Documents.Infrastructure.Common.Persistance;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ClearReadHub.Documents.Service.IntegrationTests.Common.WebApplicationFactory;

public class WebAppFactory : WebApplicationFactory<IAssemblyMarker>, IAsyncLifetime
{
    private SQLTestDatabase _testDatabase = null!;

    public AppHttpClient CreateAppHttpClient()
    {
        return new AppHttpClient(CreateClient());
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public new Task DisposeAsync()
    {
        _testDatabase.Dispose();

        return Task.CompletedTask;
    }

    public void ResetDatabase()
    {
        _testDatabase.ResetDatabase();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        _testDatabase = SQLTestDatabase.CreateAndInitialize();

        builder.ConfigureTestServices(services => services
            .RemoveAll<DbContextOptions<DocumentsDbContext>>()
            .AddDbContext<DocumentsDbContext>((sp, options) => options.UseSqlServer(_testDatabase.Connection)));

        builder.ConfigureAppConfiguration((context, conf) => conf.AddInMemoryCollection(new Dictionary<string, string?>
        {
            { "EmailSettings:EnableEmailNotifications", "false" },
        }));
    }
}