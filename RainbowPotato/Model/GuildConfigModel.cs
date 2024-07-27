using MongoDB.Bson;

namespace RainbowPotato.Model
{
    internal class GuildConfigModel : IMongoModel
    {
        public ObjectId _id { get; set; }
        public ulong guildId { get; set; }
        public string testField1 { get; set; }
        public string testField2 { get; set; }

        public GuildConfigModel(ulong guildId)
        {
            this.guildId = guildId;
            testField1 = "testField1 generated";
            testField2 = "testField2 generated";
        }
    }
}
