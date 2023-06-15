using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Presentation.Options;

internal sealed class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;
    
    
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }
    }

    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    { 
        var info = new OpenApiInfo
        {
            Title = "Kraft",
            Version = description.ApiVersion.ToString(),
            Description = "Kraft API",
            Contact = new OpenApiContact { Name = "Kraft", Email = "ezer.in@outlook.com", Url = new Uri("https://github.com") },
        };
        
        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }
        
        return info;
    }
}