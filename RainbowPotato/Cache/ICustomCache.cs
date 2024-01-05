using RainbowPotato.Model;

namespace RainbowPotato.Cache
{
    internal interface ICustomCache<T> where T : IMongoModel
    {
        T GetFromCache(string cacheKey);
        void AddIntoCache(T model, string cacheKey);
        void RemoveFromCache(string cacheKey);
    }
}
