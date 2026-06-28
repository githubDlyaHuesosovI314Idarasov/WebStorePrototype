using Serilog;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using WebStorePrototype.Server.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Keycloak.AuthServices.Authorization;
using WebStorePrototype.Server.Models;
using FluentValidation;
using Serilog.Events;
using MediatR;
using WebStorePrototype.Server.Features.Behaviors;
using Microsoft.Extensions.Caching.Hybrid;
using WebStorePrototype.Server.Models.Mapping;
using MassTransit;
using Redis.OM;
using WebStorePrototype.Server.Models.Settings;


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .WriteTo.Console()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, services, configuration) => configuration
    .MinimumLevel.Verbose()
    .WriteTo.Console());

try
{
    Log.Information("Starting web server");

    builder.Services.AddLogging();
    builder.Logging.AddSeq();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddAutoMapper(cfg => {

        cfg.AddProfile<CartProductProfile>();
        cfg.AddProfile<ComparedProductProfile>();
        cfg.AddProfile<FavoriteProductProfile>();
        cfg.AddProfile<ProductProfile>();
        cfg.AddProfile<ViewedProductProfile>();
        cfg.AddProfile<OrderProfile>();
        cfg.AddProfile<ReviewProfile>();
        cfg.AddProfile<StockProfile>();
    });

    builder.Services.AddSignalR();
    builder.Services.AddValidatorsFromAssemblyContaining<Program>();
    builder.Services.AddSwaggerGen();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddMemoryCache();
    builder.Services.AddHybridCache( options =>
    {
        options.MaximumPayloadBytes = 1024 * 1024 * 3; // 3 MB
        options.DefaultEntryOptions = new HybridCacheEntryOptions
        {
            Expiration = TimeSpan.FromMinutes(5),
        };
    });
    builder.Services.AddStackExchangeRedisCache(x => x.ConfigurationOptions = new ConfigurationOptions()
    {
        EndPoints = { builder.Configuration.GetSection("Redis:Endpoint").Value! },
        Password = builder.Configuration.GetSection("Redis:Password").Value
    });
    builder.Services.Configure<KeycloakConfiguration>(builder.Configuration.GetSection("Keycloak"));
    builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQ"));
    builder.Services.Configure<CookieOptions>(options =>
    {
        options.Expires = DateTimeOffset.UtcNow.AddDays(30);
        options.SameSite = SameSiteMode.Lax;
        options.HttpOnly = false;
        options.Secure = true;
    });
    builder.Services.AddKeycloakService(builder.Configuration);
    builder.Services.AddRedisService(builder.Configuration);
    builder.Services.AddCRMServices(builder.Configuration);
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
    builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

    var rabbitMqSettings = builder.Configuration.GetSection("RabbitMQ").Get<RabbitMQSettings>()!;

    builder.Services.AddMassTransit(config =>
    {
        config.AddConsumers(typeof(Program).Assembly);

        config.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(rabbitMqSettings.Host, h =>
            {
                h.Username(rabbitMqSettings.UserName);
                h.Password(rabbitMqSettings.Password);
            });
            cfg.ConfigureEndpoints(context);
        });
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


    if (builder.Environment.IsDevelopment())
    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddExternalWebStoreDBLocalContext(builder.Configuration);  // for local development dbs
        builder.Services.AddExternalWebStoreDbDockerContext(builder.Configuration); // for docker development db

    }
    if (builder.Environment.IsProduction())
    {
        builder.Services.AddWebStoreDBLocalContext(builder.Configuration); // for production local db
        builder.Services.AddWebStoreDbDockerContext(builder.Configuration); // for docker db production
    }


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