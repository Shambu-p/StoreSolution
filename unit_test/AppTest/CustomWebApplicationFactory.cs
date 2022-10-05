using StoreSolution.Application.common.Interfaces;
using StoreSolution.Infrastructure.Persistance;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;

namespace AppTest;

using static Testing;

internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(configurationBuilder =>
        {
            var integrationConfig = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            configurationBuilder.AddConfiguration(integrationConfig);
        });

        builder.ConfigureServices((builder, services) =>
        {
            // services
            //     .Remove<ICurrentUserService>()
            //     .AddTransient(provider => Mock.Of<ICurrentUserService>(s =>
            //         s.UserId == GetCurrentUserId()));

            services
                .Remove<DbContextOptions<ApplicationContext>>()
                .AddDbContext<IDBContext, ApplicationContext>(options =>
                    options.UseMySql(builder.Configuration.GetConnectionString("my_db"),
                        Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName))
                );
        });
    }
}