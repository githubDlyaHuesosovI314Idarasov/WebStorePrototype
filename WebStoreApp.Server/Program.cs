using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using StackExchange.Redis;
using WebStorePrototype.Server.Extensions;
using Microsoft.Extensions.Caching.StackExchangeRedis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddWebStoreDBContext(builder.Configuration);
builder.Services.AddExternalWebStoreDBContext(builder.Configuration);
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
