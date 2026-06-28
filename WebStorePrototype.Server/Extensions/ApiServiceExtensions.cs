using MassTransit.Configuration;
using Microsoft.Extensions.Options;
using Refit;
using System.Runtime;
using WebStorePrototype.Server.Models.Settings;
using WebStorePrototype.Server.Services;
using WebStorePrototype.Server.Services.CRM;
using WebStorePrototype.Server.Services.Handlers;

namespace WebStorePrototype.Server.Extensions
{
    public static class ApiServiceExtensions
    {
        public static void AddCRMServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CRMSettings>(configuration.GetSection("CRMSettings"));
            services.AddTransient<LoggingHandler>();
            services.AddTransient<ExceptionHandler>();
            services.AddTransient<RetryHandler>();
            services.AddAllCRMApiServices();
        }
        
        private static void AddAllCRMApiServices(this IServiceCollection services)
        {
            services.AddIClientAPIService();
            services.AddITransfersAPIService();
            services.AddIAgreementsAPIService();
            services.AddIContractorsAPIService();
            services.AddIDeliveriesAPIService();
            services.AddIMaterialsAPIService();
            services.AddIOfficesAPIService();
            services.AddIPaymentsAPIService();
            services.AddIProductsAPIService();
            services.AddISourcesAPIService();
            services.AddITasksAPIService();
            services.AddIUsersAPIService();
        }

        
        private static void AddIClientAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<IClientsAPIService>().ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>();
        }

        private static void AddITransfersAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<ITransfersAPIService>().ConfigureHttpClient((serviceProvider, httpClient) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>()
            .AddHttpMessageHandler<RetryHandler>();
        }

        private static void AddIAgreementsAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<IAgreementsAPIService>().ConfigureHttpClient((serviceProvider, httpClient) => {

                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>()
            .AddHttpMessageHandler<RetryHandler>();
        }

        private static void AddIContractorsAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<IContractorsAPIService>().ConfigureHttpClient((serviceProvider, httpClient) => {

                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>()
            .AddHttpMessageHandler<RetryHandler>();
        }

        private static void AddIDeliveriesAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<IDeliveriesAPIService>().ConfigureHttpClient((serviceProvider, httpClient) => {

                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>()
            .AddHttpMessageHandler<RetryHandler>();
        }

        private static void AddIMaterialsAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<IMaterialsAPIService>().ConfigureHttpClient((serviceProvider, httpClient) => {

                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>()
            .AddHttpMessageHandler<RetryHandler>();
        }

        private static void AddIOfficesAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<IOfficesAPIService>().ConfigureHttpClient((serviceProvider, httpClient) => {

                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>()
            .AddHttpMessageHandler<RetryHandler>();
        }    


        private static void AddIPaymentsAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<IPaymentsAPIService>().ConfigureHttpClient((serviceProvider, httpClient) => {

                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>()
            .AddHttpMessageHandler<RetryHandler>();
        }

        private static void AddIProductsAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<IProductsAPIService>().ConfigureHttpClient((serviceProvider, httpClient) => {

                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>()
            .AddHttpMessageHandler<RetryHandler>();
        }

        private static void AddISourcesAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<ISourcesAPIService>().ConfigureHttpClient((serviceProvider, httpClient) => {

                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>()
            .AddHttpMessageHandler<RetryHandler>();
        }

        private static void AddITasksAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<ITasksAPIService>().ConfigureHttpClient((serviceProvider, httpClient) => {

                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>()
            .AddHttpMessageHandler<RetryHandler>();
        }

        private static void AddIUsersAPIService(this IServiceCollection services)
        {
            services.AddRefitClient<IUsersAPIService>().ConfigureHttpClient((serviceProvider, httpClient) => {

                var settings = serviceProvider.GetRequiredService<IOptions<CRMSettings>>().Value;

                httpClient.BaseAddress = new Uri(settings.Entrypoint);
                httpClient.DefaultRequestHeaders.Add("X-Auth-Token", settings.ApiKey);
                httpClient.Timeout = TimeSpan.FromMinutes(4);
            })
            .AddHttpMessageHandler<LoggingHandler>()
            .AddHttpMessageHandler<ExceptionHandler>()
            .AddHttpMessageHandler<RetryHandler>();
        }
    }
}
