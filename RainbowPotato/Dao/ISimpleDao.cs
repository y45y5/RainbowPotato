using MongoDB.Bson;
using MongoDB.Driver;
using RainbowPotato.Model;

namespace RainbowPotato.Dao
{
    internal interface ISimpleDao<T> where T : ISimpleMongoModel
    {
        MongoClient GetMongoClient();
        IMongoCollection<BsonDocument> GetCollection(string collectionName);
        List<T> GetResultsFromDatabase(string cacheKey);
    }
}
