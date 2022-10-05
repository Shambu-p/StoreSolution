// using Microsoft.Extensions.Configuration;

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StoreSolution.Application.common.Interfaces;
using StoreSolution.Application.common.Models;
using StoreSolution.Infrastructure.Identity;
using StoreSolution.Infrastructure.Persistance;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration){

        services.AddDbContext<IDBContext, ApplicationContext>(options => {
            options.UseMySql(configuration.GetConnectionString("my_db"),
                Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
        });
        
        var _jwt_settings = configuration.GetSection("AuthSettings");
        services.Configure<AuthSettings>(_jwt_settings);

        var _authKey = configuration.GetValue<string>("AuthSettings:securityKey");
        services.AddAuthentication(item => {
            item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(item => {
            item.RequireHttpsMetadata = true;
            item.SaveToken = true;
            item.TokenValidationParameters = new TokenValidationParameters() {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        
        services.AddScoped<IIdentityService, IdentityService>();

        
        
        return services;

    }

}
