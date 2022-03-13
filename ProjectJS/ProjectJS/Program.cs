using ProjectJS.Domain.Entities;
using ProjectJS.Services;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddSignalR();
services.AddSingleton<Aquarium>();

var app = builder.Build();

app.UseHttpsRedirection()
    .UseFileServer()
    .UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<AquariumHub>("/aquarium");
});

app.Run();