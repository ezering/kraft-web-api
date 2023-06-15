using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth;

internal static class Extensions
{
    private const string OptionsSectionName = "Auth";
    
    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var authOptions = configuration.GetOptions<AuthOptions>(OptionsSectionName);

        services
            .Configure<AuthOptions>(configuration.GetRequiredSection(OptionsSectionName))
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Audience = authOptions.Audience;
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = authOptions.Issuer,
                    ValidAudience = authOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SigningKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        services.AddAuthorization(
            authorizationOptions =>
            {
                authorizationOptions.AddPolicy(
                    "is-admin",
                    policyBuilder =>
                    {
                        policyBuilder.RequireAuthenticatedUser();
                        policyBuilder.RequireRole("admin");
                        // policyBuilder.RequireClaim("permissions", "get-users");
                    });
                
                authorizationOptions.AddPolicy(
                    "is-user",
                    policyBuilder =>
                    {
                        policyBuilder.RequireAuthenticatedUser();
                        policyBuilder.RequireRole("user");
                    });
                
                authorizationOptions.AddPolicy(
                    "is-admin-or-user",
                    policyBuilder =>
                    {
                        policyBuilder.RequireAuthenticatedUser();
                        policyBuilder.RequireRole("admin", "user");
                    });
            });
        
        return services;
    }
}