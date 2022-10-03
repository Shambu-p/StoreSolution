// using Microsoft.Extensions.Configuration;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Infrastructure.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services){

        // services.AddScoped<IIdentityService, IdentityService>();
        return services;

    }

}
