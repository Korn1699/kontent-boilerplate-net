﻿using System.Collections.Generic;
using System.Net.Http;
using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Caching;
using Kentico.Kontent.Delivery.Caching.Webhooks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Kentico.Kontent.Boilerplate.Tests.Caching.Webhooks
{
    public class Scenario
    {
        private readonly Dictionary<string, int> _requestCounter;
        private readonly CacheManager _cacheManager;
        public CachingDeliveryClient CachingClient { get; }

        public Scenario(IMemoryCache memoryCache, HttpClient httpClient, DeliveryOptions deliveryOptions, Dictionary<string, int> requestCounter)
        {
            _requestCounter = requestCounter;
            _cacheManager = new CacheManager(memoryCache, Options.Create(new CacheOptions()));
            var baseClient = DeliveryClientBuilder.WithOptions(_ => deliveryOptions).WithHttpClient(httpClient).Build();
            CachingClient = new CachingDeliveryClient(_cacheManager, baseClient);
        }

        public void InvalidateDependency(string dependency) => _cacheManager.InvalidateDependency(dependency);

        public int GetRequestCount(string url) => _requestCounter.GetValueOrDefault(url);
    }
}