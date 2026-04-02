using Auth0.AspNetCore.Authentication;
using DAL.EF;
using Microsoft.EntityFrameworkCore;

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
            services.AddDbContext<ExternalWebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("LocalDev")));
        }

        public static void AddWebStoreDbDockerContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DockerDev")));
        }

        public static void AddExternalWebStoreDbDockerContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExternalWebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DockerDefault")));
        }

        public static void AddWebStoreDBCloudContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("CloudDev")));
        }

        public static void AddExternalWebStoreDBCloudContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExternalWebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("CloudDefault")));
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
