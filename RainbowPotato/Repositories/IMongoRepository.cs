using RainbowPotato.Model;

namespace RainbowPotato.Repositories
{
    internal interface IMongoRepository<T> where T : IMongoModel
    {
        Task<T> GetResults(string cacheKey);
    }
}
