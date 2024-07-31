using RainbowPotato.Exceptions;
using RainbowPotato.Model;
using System.Runtime.Caching;

namespace RainbowPotato.Cache
{
    internal class CustomCache<T> : ICustomCache<T> where T : IMongoModel
    {
        private readonly MemoryCache _memoryCache = MemoryCache.Default;

        public void AddToCache(T model, string cacheKey)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
            {
                // maybe it shouldn't be sliding expiration? there is an option where some configs will not be updated for a VERY long time
                // TODO think about it, changing expiration method will probably cause more database calls
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };

            _memoryCache.Add(cacheKey, model, cacheItemPolicy);
        }

        public void RemoveFromCache(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }

        public T GetFromCache(string cacheKey)
        {
            if (_memoryCache.Contains(cacheKey))
            {
                return (T)_memoryCache.Get(cacheKey);
            }

            throw new NotInCacheException();
        }
    }
}
