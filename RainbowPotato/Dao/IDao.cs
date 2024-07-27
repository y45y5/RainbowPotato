using MongoDB.Bson;
using MongoDB.Driver;
using RainbowPotato.Model;

namespace RainbowPotato.Dao
{
    internal interface IDao<T> where T : IMongoModel
    {
        MongoClient GetMongoClient();
        IMongoCollection<BsonDocument> GetCollection(string collectionName);
        T? GetResultFromDatabase(string cacheKey);
        List<T> GetResultsFromDatabase(string cacheKey);
        Task<T> AddCleanRecordToDatabase(string cacheKey);
    }
}
