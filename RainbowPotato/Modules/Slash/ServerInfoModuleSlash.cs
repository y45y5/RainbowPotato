using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using RainbowPotato.Model;
using RainbowPotato.Module.ModuleLogic;
using RainbowPotato.Repositories;

namespace RainbowPotato.Modules.Slash
{
    internal class ServerInfoModuleSlash : ApplicationCommandModule
    {
        private readonly IMongoRepository<GuildConfigModel> guildConfigRepository;
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
