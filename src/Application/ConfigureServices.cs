using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using StoreSolution.Application.common.Models;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {

    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {

        // services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
        
    }

}
