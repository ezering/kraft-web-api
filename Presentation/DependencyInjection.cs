using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Presentation.Common.Errors;
using Presentation.Common.Mapping;
using Presentation.Options;

namespace Presentation;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, KraftProblemDetailsFactory>();
        
        services.AddMappings();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();
        
        services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
                config.ApiVersionReader = new UrlSegmentApiVersionReader();
                
            });
        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });


        return services;
    }
    
    public static WebApplication UseWebApplication(this WebApplication app)
    {
        app.UseExceptionHandler("/error");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
    
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var swaggerOptions = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in swaggerOptions.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", 
                    description.GroupName.ToUpperInvariant());
            }
        });
        return app;
    }
    
    
}