using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using ZstdSharp.Unsafe;
using static MongoDB.Bson.Serialization.Serializers.SerializerHelper;

namespace RainbowPotato.Modules.GuildInfo
{
    internal class GuildInfoModuleSlash : ApplicationCommandModule
    {
        private readonly GuildInfoModuleLogic _serverInfoModuleLogic;

        public GuildInfoModuleSlash(GuildInfoModuleLogic serverInfoModuleLogic)
        {
            _serverInfoModuleLogic = serverInfoModuleLogic;
        }

        [SlashCommand("info", "General server info"), RequireGuild]
        public async Task SendGuildInformation(InteractionContext ctx, [Option("Server", "ID")] string guildId = "0")
        {
            DiscordEmbed _embed;
            ulong.TryParse(guildId, out ulong _guildId);

            if (ctx.Client.Guilds.ContainsKey(_guildId))
            {
                _embed = _serverInfoModuleLogic.GenerateGuildInfoEmbed(ctx.Client.Guilds[_guildId]);
            }
            else if (_guildId < 0)
            {
                _embed = _serverInfoModuleLogic.GenerateGuildInfoEmbed(ctx.Guild);
            }
            else
            {
                _embed = _serverInfoModuleLogic.GenerateGuildNotFoundEmbed(ctx.Client, _guildId);
            }

            DiscordMessageBuilder discordMessageBuilder = new DiscordMessageBuilder
            {
                Embed = _embed
            };

            await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder(discordMessageBuilder));
        }
    }
}
