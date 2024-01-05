using RainbowPotato.Model;

namespace RainbowPotato.Repositories
{
    internal interface IMongoRepository<T> where T : IMongoModel
    {
        T GetResultForCacheKey(string cacheKey);
        Task CreateNewRecordInDatabaseAsync(ulong guildId);
    }
}
