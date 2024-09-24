using MongoDB.Bson;

namespace RainbowPotato.Model
{
    internal interface ISimpleMongoModel
    {
        ObjectId _id { get; set; }
    }
}
