using DSharpPlus;
using DSharpPlus.Entities;
using RainbowPotato.Model;
using RainbowPotato.Repositories;

namespace RainbowPotato.Modules.ServerInfo
{
    internal class GuildInfoModuleLogic
    {
        private readonly IMongoRepository<GuildConfigModel> _guildConfigRepository;

        public GuildInfoModuleLogic(IMongoRepository<GuildConfigModel> guildConfigRepository)
        {
            _guildConfigRepository = guildConfigRepository;

            //GuildConfigModel guildConfig = await _guildConfigRepository.GetResults($"GuildConfig#{guild.Id}");
        }

        public DiscordEmbed GenerateGuildInfoEmbed(DiscordGuild guild)
        {
            IReadOnlyCollection<DiscordChannel> allChannels = guild.GetChannelsAsync().Result;
            int categoriesChannels = allChannels.Where(channel => channel.IsCategory).Count();
            int voiceChannels = allChannels.Where(channel => channel.Type == ChannelType.Voice).Count();
            int textChannels = allChannels.Count - categoriesChannels - voiceChannels;

            IReadOnlyCollection<DiscordMember> allMembers = guild.GetAllMembersAsync().Result;
            int botMembers = allMembers.Where(member => member.IsBot).Count();
            int realMembers = allMembers.Count - botMembers;

            IReadOnlyDictionary<ulong, DiscordRole> allRoles = guild.Roles;
            int adminRoles = allRoles.ToList().Where(role => role.Value.Permissions.ToPermissionString().Contains("Administrator")).Count();

            IReadOnlyDictionary<ulong, DiscordEmoji> allEmojis = guild.Emojis;
            int animatedEmojis = allEmojis.ToList().Where(emoji => emoji.Value.IsAnimated).Count();

            int normalEmojis = allEmojis.Count - animatedEmojis;

            EmbedBuilder embedBuilder = new();
            embedBuilder.AddField("Server name", $"```{guild.Name}```", true);
            embedBuilder.AddField("Server ID", $"```{guild.Id}```", true);
            embedBuilder.AddField("Server members", $"```all:{allMembers.Count} members:{realMembers} bots:{botMembers}```", false);
            embedBuilder.AddField("Owner name", $"```{guild.Owner.DisplayName}```", true);
            embedBuilder.AddField("Owner ID", $"```{guild.Owner.Id}```", true);
            embedBuilder.AddField("Server channels", $"```all:{allChannels.Count} text:{textChannels} voice:{voiceChannels} categories:{categoriesChannels}```", false);
            embedBuilder.AddField("Server emojis", $"```all:{allEmojis.Count} animated:{animatedEmojis} normal:{normalEmojis}```", false);
            embedBuilder.AddField("Server roles", $"```all:{allRoles.Count} admin:{adminRoles}```", false);
            embedBuilder.AddField("Server created on (MM/DD/YYYY)", $"```{guild.CreationTimestamp.ToString("MM/dd/yyyy HH:mm")}```", false);
            embedBuilder.SetThumbnail(guild.IconUrl);

            return embedBuilder.Build();
        }

        public DiscordEmbed GenerateGuildNotFoundEmbed(DiscordClient client, ulong guildId)
        {
            EmbedBuilder embedBuilder = new();
            embedBuilder.AddField("What are you looking for?", $"```Server ID#{guildId} not found```", true);
            embedBuilder.SetThumbnail(client.CurrentUser.AvatarUrl);

            return embedBuilder.Build();
        }
    }
}
