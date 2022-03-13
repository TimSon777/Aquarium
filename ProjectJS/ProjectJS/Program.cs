var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddSignalR();

var app = builder.Build();

app.UseHttpsRedirection()
    .UseFileServer();

app.Run();