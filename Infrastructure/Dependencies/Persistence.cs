using Application.Abstractions.Time;
using Domain.Abstractions;
using Infrastructure.DAL;
using Infrastructure.DAL.Repositories;
using Infrastructure.DAL.Services;
using Infrastructure.Time;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Dependencies;

public static class Persistence
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddSingleton<IClock, Clock>();
        services.AddScoped<IUnitOfWork>(
            factory => factory.GetRequiredService<ApplicationDbContext>());
        
        return services;
    }
}