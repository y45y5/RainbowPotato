using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using RainbowPotato.Model;

namespace RainbowPotato.Dao
{
    internal class Dao<T> : IDao<T> where T : IMongoModel
    {
        private string collectionName;

        public Dao(string collectionName)
        {
            this.collectionName = collectionName;
        }

        public MongoClient GetMongoClient()
        {
            return new MongoClient(Variables.mongoClientString);
        }

        public IMongoCollection<BsonDocument> GetCollection()
        {
            MongoClient mongoClient = GetMongoClient();
            IMongoDatabase database = mongoClient.GetDatabase(Variables.daoDataBaseName);
            IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>(collectionName);
            return collection;
        }

        public T? GetOneResultFromDatabase(ulong guildId)
        {
            IMongoCollection<BsonDocument> collection = GetCollection();
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("guildId", guildId);
            BsonDocument bsonDocument = collection.Find(filter).FirstOrDefault();

            if (bsonDocument == null)
            {
                return default;
            }

            T result = BsonSerializer.Deserialize<T>(bsonDocument);
            return result;
        }

        public List<T> GetAllResultsFromDatabase()
        {
            IMongoCollection<BsonDocument> collection = GetCollection();
            List<BsonDocument> bsonDocuments = collection.Find(_ => true).ToList();
            List<T> results = new List<T>();
            bsonDocuments.ForEach(x => results.Add(BsonSerializer.Deserialize<T>(x)));
            return results;
        }

        public async Task InsertCleanRecordIntoDatabaseAsync(ulong guildId)
        {
            IMongoCollection<BsonDocument> collection = GetCollection();
            T cleanModel = Activator.CreateInstance<T>();
            cleanModel.guildId = guildId;
            await collection.InsertOneAsync(cleanModel.ToBsonDocument());
        }
    }
}
