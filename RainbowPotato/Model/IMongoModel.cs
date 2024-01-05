using MongoDB.Bson;

namespace RainbowPotato.Model
{
    internal interface IMongoModel
    {
        ObjectId _id { get; set; }
        ulong guildId { get; set; }
    }
}
