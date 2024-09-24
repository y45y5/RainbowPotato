using MongoDB.Bson;

namespace RainbowPotato.Model
{
    internal class ImgurAlbumTriggerModel : ISimpleMongoModel
    {
        public ObjectId _id { get; set; }
        public string trigger { get; set; }
        public string albumHash { get; set; }

        public ImgurAlbumTriggerModel()
        {
            
        }
    }
}
