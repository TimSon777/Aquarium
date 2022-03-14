using ProjectJS.Domain.Entities;
using ProjectJS.SignalR;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddSignalR();
services.AddSingleton<Aquarium>();
services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection()
    .UseFileServer()
    .UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<AquariumHub>("/aquarium");
    endpoints.MapControllers();
});

app.Run();