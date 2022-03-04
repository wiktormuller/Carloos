using System;
using Ardalis.GuardClauses;
using JobJetRestApi.Application.Ports;
using Microsoft.Extensions.Caching.Memory;

namespace JobJetRestApi.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = Guard.Against.Null(memoryCache, nameof(memoryCache));
        }

        public void Add<TItem>(TItem item, string cacheKey)
        {
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(15),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(12)
            };
            
            _memoryCache.Set(cacheKey, item, cacheExpiryOptions);
        }

        public TItem Get<TItem>(string cacheKey) where TItem : class
        {
            return _memoryCache.TryGetValue(cacheKey, out TItem result) ? result : null;
        }
    }
}