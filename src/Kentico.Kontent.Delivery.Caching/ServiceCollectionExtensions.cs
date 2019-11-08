using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Kentico.Kontent.Delivery.Caching
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCachingClient(this IServiceCollection services, Func<IServiceProvider, IDeliveryClient> baseClientFactory, Action<CacheOptions> configureCacheOptions = null)
        {
            if (configureCacheOptions != null)
            {
                services.Configure(configureCacheOptions);
            }
            services.TryAddSingleton<Default.ICacheManager, Default.CacheManager>();

            services.AddSingleton<IDeliveryClient>(sp => new Default.CachingDeliveryClient(
                sp.GetRequiredService<Default.ICacheManager>(),
                baseClientFactory(sp)));
            return services;
        }

        public static IServiceCollection AddWebhookInvalidatedCachingClient(this IServiceCollection services, Func<IServiceProvider, IDeliveryClient> baseClientFactory, Action<CacheOptions> configureCacheOptions = null)
        {
            if (configureCacheOptions != null)
            {
                services.Configure(configureCacheOptions);
            }
            services.TryAddSingleton<Webhooks.ICacheManager, Webhooks.CacheManager>();

            services.AddSingleton<IDeliveryClient>(sp => new Webhooks.CachingDeliveryClient(
                sp.GetRequiredService<Webhooks.ICacheManager>(),
                baseClientFactory(sp)));
            return services;
        }
    }
}