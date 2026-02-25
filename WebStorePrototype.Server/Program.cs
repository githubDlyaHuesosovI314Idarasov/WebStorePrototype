using DAL.EF;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using StackExchange.Redis;
using WebStorePrototype.Server.Extensions;
using Microsoft.Extensions.Caching.StackExchangeRedis;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
try
{
    Log.Information("Starting web server");

    builder.Services.AddLogging();
    builder.Logging.AddSeq();
    builder.Logging.AddSerilog();
    builder.Services.AddControllers();
    builder.Services.AddOpenApi();
    builder.Services.AddWebStoreDBContext(builder.Configuration);
    builder.Services.AddExternalWebStoreDBContext(builder.Configuration);
    builder.Services.Add0Auth(builder.Configuration);
    builder.Services.AddStackExchangeRedisCache(x => x.ConfigurationOptions = new ConfigurationOptions()
    {
        EndPoints = { builder.Configuration.GetConnectionString("Redis")! },
        Password = ""
    });
    var app = builder.Build();

    app.UseDefaultFiles();
    app.MapStaticAssets();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.UseExceptionHandler("/error");
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.MapFallbackToFile("/index.html");

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}