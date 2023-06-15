using Infrastructure.DAL.Services;
using Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "postgres";
    public static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresOptions>(configuration.GetRequiredSection(OptionsSectionName));
        var postgresOptions = configuration.GetOptions<PostgresOptions>(OptionsSectionName);
        services.AddDbContext<ApplicationDbContext>(x 
            => x.UseNpgsql(postgresOptions.ConnectionString));
        
        services.AddHostedService<DatabaseInitializer>();

        return services;
    }
}