using Application.Auth;
using Infrastructure.Auth;
using Infrastructure.DAL;
using Infrastructure.Dependencies;
using Infrastructure.Options;
using Infrastructure.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppOptions>(configuration.GetRequiredSection("app"));
        services.AddHttpContextAccessor();
        services.AddSingleton<IAuthenticator, Authenticator>();
        services.AddSingleton<ITokenStorage, HttpContextTokenStorage>();
        
        services
            .AddPostgres(configuration)
            .AddPersistence()
            .AddSecurity()
            .AddAuth(configuration);
        return services;
    }

    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetRequiredSection(sectionName);
        section.Bind(options);
        return options;
    }
}