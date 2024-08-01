using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace RainbowPotato.Modules.ServerInfo
{
    internal class ServerInfoModuleCommands : BaseCommandModule
    {
        private readonly ServerInfoModuleLogic _serverInfoModuleLogic;

        public ServerInfoModuleCommands(ServerInfoModuleLogic serverInfoModuleLogic)
        {
            _serverInfoModuleLogic = serverInfoModuleLogic;
        }

        [Command("info"), RequireGuild, Hidden]
        public async Task SendGuildStatistics(CommandContext ctx)
        {
            await ctx.RespondAsync(_serverInfoModuleLogic.GetServerInformation(ctx.Guild));
        }
    }
}
