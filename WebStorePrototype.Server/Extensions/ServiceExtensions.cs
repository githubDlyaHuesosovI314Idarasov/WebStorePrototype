using DAL.EF;
using Microsoft.EntityFrameworkCore;
using Polly;
using StackExchange.Redis;
using Microsoft.Extensions.Http.Resilience;
using WebStorePrototype.Server.Services;
using System.Net;
using Polly.Retry;
using Polly.CircuitBreaker;
using Microsoft.Extensions.Caching.Hybrid;

namespace WebStorePrototype.Server.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }

        public static void AddWebStoreDBLocalContext(this IServiceCollection services, IConfiguration configuration) // for release
        {
            services.AddDbContext<WebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("LocalDefault")));
        }

        public static void AddExternalWebStoreDBLocalContext(this IServiceCollection services, IConfiguration configuration) // for development
        {
            services.AddDbContext<WebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("LocalDev")));
        }

        public static void AddWebStoreDbDockerContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DockerDefault")));
        }

        public static void AddExternalWebStoreDbDockerContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DockerDev")));
        }

        public static void AddWebStoreDBCloudContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("CloudDev")));
        }

        public static void AddExternalWebStoreDBCloudContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("CloudDefault")));
        }

        public static void AddHybridCache(this IServiceCollection services)
        {
            services.AddHybridCache(options =>
            {
                options.DefaultEntryOptions = new HybridCacheEntryOptions
                {
                    Expiration = TimeSpan.FromMinutes(1),
                    LocalCacheExpiration = TimeSpan.FromSeconds(15),
                };


            });
        }


        // This method is commented out because the project has been switched to Keycloak for authentication, but it can be used as a reference for adding Auth0 authentication in the future if needed.
        /*
        public static void Add0Auth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuth0WebAppAuthentication(options =>
            {
                options.Domain = configuration["Auth0:Domain"];
                options.ClientId = configuration["Auth0:ClientId"];
                options.ClientSecret = configuration["Auth0:ClientSecret"];
            });
        }
        */
    }
}
