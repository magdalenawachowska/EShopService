using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Service
{
    public class CacheService             
    {

        private readonly IMemoryCache _cache;

        public CacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string GetValue()
        {
            string key = "klucz";

            if (!_cache.TryGetValue(key, out string cachedValue))
            {
                cachedValue = "to jest przykładowa wartość";

                var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(5));

                _cache.Set(key, cachedValue, options);
            }

            return cachedValue;
        }
    }
}