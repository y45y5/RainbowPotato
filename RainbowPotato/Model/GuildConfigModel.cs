using MongoDB.Bson;

namespace RainbowPotato.Model
{
    internal class GuildConfigModel : IMongoModel
    {
        public ObjectId _id { get; set; }
        public ulong guildId { get; set; }
        public int kickAfterDays { get; set; }
        public ulong mainTextChannelId { get; set; }
        public ulong welcomeChannelId { get; set; }
        public ulong verifiedRoleId { get; set; }
        public bool editNickNamesOnJoin { get; set; }
        public bool autoKickAfterDateExpires { get; set; }
        public string nickNameTemplate { get; set; }
        public ulong mainGuildId { get; set; }

        public GuildConfigModel(ulong guildId)
        {
            this.guildId = guildId;
            kickAfterDays = 4;
            mainTextChannelId = 0;
            welcomeChannelId = 0;
            verifiedRoleId = 0;
            editNickNamesOnJoin = false;
            autoKickAfterDateExpires = false;
            nickNameTemplate = "・[nickname]・";
            mainGuildId = 0;
        }
    }
}
