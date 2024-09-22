﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace RainbowPotato.Modules.AdminTools
{
    internal class DevToolsModuleCommands : BaseCommandModule
    {
        private readonly DevToolsModuleLogic _adminToolsLogic;

        public DevToolsModuleCommands(DevToolsModuleLogic adminToolsLogic)
        {
            _adminToolsLogic = adminToolsLogic;
        }

        [Command("clearcache"), Hidden]
        public async Task ClearCache(CommandContext ctx)
        {
            _adminToolsLogic.ClearCache(ctx.User.Id);

            CustomUtils.AddSuccessReaction(ctx.Client, ctx.Message);
        }
    }
}
