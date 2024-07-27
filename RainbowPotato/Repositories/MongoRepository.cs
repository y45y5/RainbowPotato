using RainbowPotato.Cache;
using RainbowPotato.Dao;
using RainbowPotato.Exceptions;
using RainbowPotato.Model;

namespace RainbowPotato.Repositories
{
    internal class MongoRepository<T> : IMongoRepository<T> where T : IMongoModel
    {
        private readonly IDao<T> mongoDao;
        private readonly ICustomCache<T> customCache;

        public MongoRepository(IDao<T> mongoDao, ICustomCache<T> customCache)
        {
            this.mongoDao = mongoDao;
            this.customCache = customCache;
        }

        // Return the model for a given cache key. Repo first searches the cache, 
        // if the model isn't there, it sends a query to the database, 
        // if it isn't in the database either, it creates a new, empty model and puts it in the database, then adds it to the cache.

        // If you spam some commands right after bot launch, it will create multiple clean models and insert all of them into the databse, TODO how to fix it????? good enough for now
        public async Task<T> GetResults(string cacheKey)
        {
            try
            {
                return customCache.GetFromCache(cacheKey);
            }
            catch (NotInCacheException)
            {
                T? result = mongoDao.GetResultFromDatabase(cacheKey);
                result ??= await mongoDao.AddCleanRecordToDatabase(cacheKey);
                customCache.AddToCache(result, cacheKey);
                return result;
            }
        }
    }
}
