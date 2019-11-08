using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kentico.Kontent.Delivery.Caching.Webhooks
{
    public interface ICacheManager
    {
        Task<T> GetOrAddAsync<T>(string key, Func<Task<T>> valueFactory, Func<T, IEnumerable<string>> dependenciesFactory, Func<T, bool> shouldCache = null);
        bool TryGet<T>(string key, out T value);
        void InvalidateDependency(string key);
    }
}