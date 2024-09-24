using RainbowPotato.Cache;
using RainbowPotato.Dao;
using RainbowPotato.Exceptions;
using RainbowPotato.Model;

namespace RainbowPotato.Repositories
{
    internal class MongoRepository<T> : IMongoRepository<T> where T : IMongoModel
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private readonly IDao<T> _mongoDao;
        private readonly ICustomCache<T> _customCache;

        public MongoRepository(IDao<T> mongoDao, ICustomCache<T> customCache)
        {
            _mongoDao = mongoDao;
            _customCache = customCache;
        }

        // Return the model for a given cache key.

        // 1. Check semaphore
        // 2. Try to get config from cache, if in cache - return
        // 3. Try to get config from database, if in database - insert into cache and return
        // 4. Try to add new clean config into database - insert into cache and return
        // Release semaphore at the end
        public async Task<T> GetResults(string cacheKey)
        {
            await _semaphore.WaitAsync();

            try
            {
                return _customCache.GetFromCache(cacheKey);
            }
            catch (NotInCacheException)
            {
                T? result = _mongoDao.GetResultFromDatabase(cacheKey);
                result ??= await _mongoDao.AddCleanRecordToDatabase(cacheKey);
                _customCache.AddToCache(result, cacheKey);

                return result;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        public async void ClearCache()
        {
            await _semaphore.WaitAsync();

            try
            {
                _customCache.Clear();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
