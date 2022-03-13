var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

var app = builder.Build();

app.UseHttpsRedirection()
    .UseFileServer();

app.Run();