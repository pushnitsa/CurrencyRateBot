using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace CurrencyRateBot.Cache.Extensions
{
    public static class MemoryCacheExtensions
    {

        public static async Task<TEntry> GetOrAddAsync<TEntry>(this IMemoryCache memoryCache, string key, Func<ICacheEntry, Task<TEntry>> factory)
        {
            return await memoryCache.GetOrCreateAsync(key, factory);
        }

        public static TEntry GerOrAdd<TEntry>(this IMemoryCache memoryCache, string key, Func<ICacheEntry, TEntry> factory)
        {
            return memoryCache.GetOrCreate(key, factory);
        }
    }
}
