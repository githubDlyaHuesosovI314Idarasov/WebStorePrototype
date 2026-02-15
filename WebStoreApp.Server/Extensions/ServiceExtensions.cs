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
    }
}
