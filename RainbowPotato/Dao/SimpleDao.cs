using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using RainbowPotato.Model;

namespace RainbowPotato.Dao
{
    internal class SimpleDao<T> : ISimpleDao<T> where T : ISimpleMongoModel
    {
        public MongoClient GetMongoClient()
        {
            return new MongoClient(Settings.mongoClientString);
        }

        public IMongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            MongoClient mongoClient = GetMongoClient();
            IMongoDatabase database = mongoClient.GetDatabase(Settings.daoDataBaseName);
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);

            return collection;
        }

        public List<T> GetResultsFromDatabase(string cacheKey)
        {
            IMongoCollection<BsonDocument> collection = GetCollection(CustomUtils.GetCollectionNameFromCacheKey(cacheKey));
            List<BsonDocument> bsonDocuments = collection.Find(_ => true).ToList();
            List<T> results = new();
            bsonDocuments.ForEach(x => results.Add(BsonSerializer.Deserialize<T>(x)));

            return results;
        }
    }
}
