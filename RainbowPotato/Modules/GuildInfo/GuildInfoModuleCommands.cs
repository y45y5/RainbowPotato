using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;

namespace RainbowPotato.Modules.GuildInfo
{
    internal class GuildInfoModuleCommands : BaseCommandModule
    {
        private readonly GuildInfoModuleLogic _serverInfoModuleLogic;

        public GuildInfoModuleCommands(GuildInfoModuleLogic serverInfoModuleLogic)
        {
            _serverInfoModuleLogic = serverInfoModuleLogic;
        }

        [Command("info"), RequireGuild, Hidden]
        public async Task SendGeneralServerInfo(CommandContext ctx, [RemainingText] ulong guildId)
        {
            if (ctx.Client.Guilds.ContainsKey(guildId))
            {
                await ctx.RespondAsync(_serverInfoModuleLogic.GenerateGuildInfoEmbed(ctx.Client.Guilds[guildId]));
            }
            else if (guildId <= 0)
            {
                await ctx.RespondAsync(_serverInfoModuleLogic.GenerateGuildInfoEmbed(ctx.Guild));
            }
            else
            {
                await ctx.RespondAsync(_serverInfoModuleLogic.GenerateGuildNotFoundEmbed(ctx.Client, guildId));
            }
        }
    }
}
