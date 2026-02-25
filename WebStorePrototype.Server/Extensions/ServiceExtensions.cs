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

        public static void AddWebStoreDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Default")));
        }

        public static void AddExternalWebStoreDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ExternalWebStoreDBContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("Dev")));
        }

        public static void Add0Auth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuth0WebAppAuthentication(options =>
            {
                options.Domain = configuration["Auth0:Domain"];
                options.ClientId = configuration["Auth0:ClientId"];
                options.ClientSecret = configuration["Auth0:ClientSecret"];
            });
        }
    }
}
