using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace RainbowPotato.Modules.ServerInfo
{
    internal class ServerInfoModuleSlash : ApplicationCommandModule
    {
        private readonly ServerInfoModuleLogic _serverInfoModuleLogic;

        public ServerInfoModuleSlash(ServerInfoModuleLogic serverInfoModuleLogic)
        {
            _serverInfoModuleLogic = serverInfoModuleLogic;
        }

        [SlashCommand("info", "Get some server info")]
        [RequireGuild]
        public async Task SendServerInformation(InteractionContext ctx)
        {
            DiscordMessageBuilder discordMessageBuilder = new DiscordMessageBuilder
            {
                Embed = _serverInfoModuleLogic.GetServerInformation(ctx.Guild)
            };
            await ctx.CreateResponseAsync(new DiscordInteractionResponseBuilder(discordMessageBuilder));
        }
    }
}
