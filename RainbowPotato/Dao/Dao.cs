using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using RainbowPotato.Model;

namespace RainbowPotato.Dao
{
    internal class Dao<T> : IDao<T> where T : IMongoModel
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

        public T? GetResultFromDatabase(string cacheKey)
        {
            IMongoCollection<BsonDocument> collection = GetCollection(Tools.GetCollectionNameFromCacheKey(cacheKey));
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("guildId", Tools.GetGuildIdFromCacheKey(cacheKey));
            BsonDocument bsonDocument = collection.Find(filter).FirstOrDefault();

            if (bsonDocument == null)
            {
                return default;
            }

            T result = BsonSerializer.Deserialize<T>(bsonDocument);
            return result;
        }

        public List<T> GetResultsFromDatabase(string cacheKey)
        {
            IMongoCollection<BsonDocument> collection = GetCollection(Tools.GetCollectionNameFromCacheKey(cacheKey));
            List<BsonDocument> bsonDocuments = collection.Find(_ => true).ToList();
            List<T> results = new();
            bsonDocuments.ForEach(x => results.Add(BsonSerializer.Deserialize<T>(x)));
            return results;
        }

        public async Task<T> AddCleanRecordToDatabase(string cacheKey)
        {
            IMongoCollection<BsonDocument> collection = GetCollection(Tools.GetCollectionNameFromCacheKey(cacheKey));
            T? cleanModel = (T?)Activator.CreateInstance(typeof(T), Tools.GetGuildIdFromCacheKey(cacheKey));

            if (cleanModel == null)
            {
                throw new InvalidOperationException($"Cannot create an instance of {typeof(T)}.");
            }

            cleanModel.guildId = Tools.GetGuildIdFromCacheKey(cacheKey);
            await collection.InsertOneAsync(cleanModel.ToBsonDocument());
            return cleanModel;
        }
    }
}
