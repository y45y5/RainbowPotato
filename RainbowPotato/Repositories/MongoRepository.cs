using RainbowPotato.Cache;
using RainbowPotato.Dao;
using RainbowPotato.Exceptions;
using RainbowPotato.Model;

namespace RainbowPotato.Repositories
{
    internal class MongoRepository<T> : IMongoRepository<T> where T : IMongoModel
    {
        private readonly IDao<T> mongoDao;
        private ICustomCache<T> cacheService;

        public MongoRepository(IDao<T> mongoDao, ICustomCache<T> cacheService)
        {
            this.mongoDao = mongoDao;
            this.cacheService = cacheService;
        }

        public async Task CreateNewRecordInDatabaseAsync(ulong guildId)
        {
            T? result = mongoDao.GetOneResultFromDatabase(guildId);

            if (result == null)
            {
                await mongoDao.InsertCleanRecordIntoDatabaseAsync(guildId);
            }
        }

        public T GetResultForCacheKey(string cacheKey)
        {
            try
            {
                return cacheService.GetFromCache(cacheKey);
            }
            catch (NotInCacheException)
            {
                ulong guildId = ulong.Parse(cacheKey.Substring(cacheKey.IndexOf("#") + 1));
                T? result = mongoDao.GetOneResultFromDatabase(guildId);

                if (result == null)
                {
                    result = Activator.CreateInstance<T>();
                    result.guildId = guildId;
                }

                cacheService.AddIntoCache(result, cacheKey);
                return result;
            }
        }
    }
}
