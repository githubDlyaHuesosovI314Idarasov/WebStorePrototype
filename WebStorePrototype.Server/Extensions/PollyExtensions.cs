using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using StackExchange.Redis;
using System.Net;
using WebStorePrototype.Server.Services;

namespace WebStorePrototype.Server.Extensions
{
    public static class PollyExtensions
    {
        public static IServiceCollection AddKeycloakService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<KeycloakUserService>().AddResilienceHandler("keycloak-pipeline", pipeline =>
            {
                pipeline.AddRetry(new HttpRetryStrategyOptions
                {
                    MaxRetryAttempts = 4,
                    BackoffType = DelayBackoffType.Exponential,
                    Delay = TimeSpan.FromSeconds(4),
                    ShouldHandle = args => args.Outcome switch
                    {
                        { Exception: HttpRequestException } => PredicateResult.True(),
                        { Result.StatusCode: HttpStatusCode.ServiceUnavailable } => PredicateResult.True(),
                        { Result.StatusCode: HttpStatusCode.TooManyRequests } => PredicateResult.True(),
                        _ => PredicateResult.False(),
                    }
                });

                pipeline.AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions
                {
                    SamplingDuration = TimeSpan.FromSeconds(30),
                    MinimumThroughput = 5,
                    FailureRatio = 0.5,
                    BreakDuration = TimeSpan.FromSeconds(15)
                });

                pipeline.AddTimeout(TimeSpan.FromSeconds(5));
            });
            return services;
        }

        public static IServiceCollection AddRedisService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!));
            services.AddSingleton(typeof(RedisService<>));

            services.AddResiliencePipeline("redis-pipeline", pipeline => {

                pipeline.AddRetry(new RetryStrategyOptions()
                {
                    MaxRetryAttempts = 4,
                    BackoffType = DelayBackoffType.Exponential,
                    Delay = TimeSpan.FromSeconds(4),
                    ShouldHandle = new PredicateBuilder().Handle<RedisException>().Handle<TimeoutException>()
                });

                pipeline.AddCircuitBreaker(new CircuitBreakerStrategyOptions
                {
                    SamplingDuration = TimeSpan.FromSeconds(30),
                    MinimumThroughput = 5,
                    FailureRatio = 0.5,
                    BreakDuration = TimeSpan.FromSeconds(15)
                });


                pipeline.AddTimeout(TimeSpan.FromSeconds(5));
            });

            return services;

        }
    }
}
