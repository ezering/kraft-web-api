using Application;
using Infrastructure;
using Presentation;

var builder = WebApplication.CreateBuilder(args);
builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration)
        .AddPresentation();


var app = builder.Build();
app.UseWebApplication();
app.Run();




