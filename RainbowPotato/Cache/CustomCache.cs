﻿using RainbowPotato.Exceptions;
using RainbowPotato.Model;
using System.Runtime.Caching;

namespace RainbowPotato.Cache
{
    internal class CustomCache<T> : ICustomCache<T> where T : IMongoModel
    {
        private readonly MemoryCache memoryCache = MemoryCache.Default;

        public void AddToCache(T model, string cacheKey)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy
            {
                SlidingExpiration = TimeSpan.FromMinutes(10)
            };

            memoryCache.Add(cacheKey, model, cacheItemPolicy);
        }

        public void RemoveFromCache(string cacheKey)
        {
            memoryCache.Remove(cacheKey);
        }

        public T GetFromCache(string cacheKey)
        {
            if (memoryCache.Contains(cacheKey))
            {
                return (T)memoryCache.Get(cacheKey);
            }

            throw new NotInCacheException();
        }
    }
}
