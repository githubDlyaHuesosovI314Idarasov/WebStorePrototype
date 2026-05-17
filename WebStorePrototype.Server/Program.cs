using DAL.EF;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using StackExchange.Redis;
using WebStorePrototype.Server.Extensions;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Keycloak.AuthServices.Authorization;
using WebStorePrototype.Server.Models;
using Microsoft.Extensions.Caching.Memory;
using WebStorePrototype.Server.Services;
using WebStorePrototype.Server.Services.Base;
using Serilog.Events;
using MediatR;
using WebStorePrototype.Server.Features.Behaviors;
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
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddKeycloakService(builder.Configuration);
    builder.Services.AddRedisService(builder.Configuration);

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    builder.Services.AddScoped<IFavoriteProductsService, FavoriteProductsService>();
    builder.Services.AddScoped<IViewedProductsService, ViewedProductService>();
    builder.Services.Configure<KeycloakConfiguration>(builder.Configuration.GetSection("Keycloak"));
    builder.Services.Configure<CookieOptions>(options =>
    {
        options.Expires = DateTimeOffset.UtcNow.AddDays(30);
        options.SameSite = SameSiteMode.Lax;
        options.HttpOnly = false;
        options.Secure = true;
    });
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = "http://localhost:8080/realms/WebStoreServerRealm";
            options.Audience = "web-api";
            options.RequireHttpsMetadata = false;       
            options.TokenValidationParameters = new TokenValidationParameters 
            {
                ValidateIssuer = true,
                ValidIssuer = "http://localhost:8080/realms/WebStoreServerRealm",
                ValidateAudience = true,
                ValidAudience = "web-api",
                ValidateLifetime = true,
            };
        });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminOnly", policy => policy.RequireRealmRoles("admin"));
    });
    builder.Services.AddControllers();
    builder.Services.AddSignalR();

    builder.Services.AddExternalWebStoreDBLocalContext(builder.Configuration); // for local development db
    builder.Services.AddWebStoreDBLocalContext(builder.Configuration); // for production local db
    builder.Services.AddExternalWebStoreDbDockerContext(builder.Configuration); // for docker db production
    builder.Services.AddWebStoreDbDockerContext(builder.Configuration); // for docker db dev

    // builder.Services.AddExternalWebStoreDBCloudContext(builder.Configuration);
    // builder.Services.AddWebStoreDBCloudContext(builder.Configuration);
    // This method is commented out because the project has been switched to Keycloak for authentication, but it can be used as a reference for adding Auth0 authentication in the future if needed.
    // builder.Services.Add0Auth(builder.Configuration);
    builder.Services.AddStackExchangeRedisCache(x => x.ConfigurationOptions = new ConfigurationOptions()
    {
        EndPoints = { builder.Configuration.GetConnectionString("Redis")! },
        Password = ""
    });
    var app = builder.Build();

    
    app.UseDefaultFiles();
    app.MapStaticAssets();
    app.UseSerilogRequestLogging(options =>
    {
        options.MessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";
        options.GetLevel = (httpContext, elapsed, ex) =>
           httpContext.Request.Path.StartsWithSegments("/health") ? LogEventLevel.Verbose : LogEventLevel.Information;
        
    });    

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseExceptionHandler("/error");
    }

    app.UseHttpsRedirection();

    app.UseAuthentication();
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