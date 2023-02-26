using Microsoft.Extensions.Caching.Memory;
using MimunYashirCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimunYashirCore.Services
{
    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IMemoryCache _cache;
        public MemoryCacheService(
            IMemoryCache cache)
        {
            _cache = cache;
        }

        public void DeleteCache(string key)
        {
            _cache.Remove(key);
        }

        public object? TryGetObjectFromCache(string key)
        {
            _cache.TryGetValue(key, out var value);
            return value;
        }
        public void UpdateOrCreateCache(string key, object value, int slidingMin = 5, int absoluteMin = 30)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(slidingMin))
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(absoluteMin));
            _cache.Set(key, value, cacheEntryOptions);
        }
        
    }
}
