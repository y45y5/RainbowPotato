using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using RainbowPotato.Model;
using RainbowPotato.Repositories;

namespace RainbowPotato.Modules.ServerInfo
{
    internal class ServerInfoModuleSlash : ApplicationCommandModule
    {
        private readonly IMongoRepository<GuildConfigModel> _guildConfigRepository;
        private readonly ServerInfoModuleLogic statisticsModuleLogic = new();

        [SlashCommand("info", "Get some server info")]
        [RequireGuild]
        public async Task SendServerInformation(InteractionContext ctx)
        {
            DiscordMessageBuilder discordMessageBuilder = new DiscordMessageBuilder
            {
                Embed = statisticsModuleLogic.GetServerInformation(ctx.Guild)
            };
            await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder(discordMessageBuilder));
        }
    }
}
