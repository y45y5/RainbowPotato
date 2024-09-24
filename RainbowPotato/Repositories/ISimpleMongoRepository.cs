using RainbowPotato.Model;

namespace RainbowPotato.Repositories
{
    internal interface ISimpleMongoRepository<T> where T : ISimpleMongoModel
    {
        Task<List<T>> GetResults(string cacheKey);
    }
}
