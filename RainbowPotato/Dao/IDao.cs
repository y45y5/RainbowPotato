using MongoDB.Bson;
using MongoDB.Driver;
using RainbowPotato.Model;

namespace RainbowPotato.Dao
{
    internal interface IDao<T> where T : IMongoModel
    {
        MongoClient GetMongoClient();
        IMongoCollection<BsonDocument> GetCollection();
        T? GetOneResultFromDatabase(ulong guildId);
        List<T> GetAllResultsFromDatabase();
        Task InsertCleanRecordIntoDatabaseAsync(ulong guildId);
    }
}
