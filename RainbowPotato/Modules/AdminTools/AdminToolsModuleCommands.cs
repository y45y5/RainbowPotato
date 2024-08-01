using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace RainbowPotato.Modules.AdminTools
{
    internal class AdminToolsModuleCommands
    {
        private readonly AdminToolsModuleLogic _adminToolsLogic;

        public AdminToolsModuleCommands(AdminToolsModuleLogic adminToolsLogic) 
        {
            _adminToolsLogic = adminToolsLogic;
        }

        [Command("clearcache"), Hidden]
        public void ClearCache(CommandContext ctx)
        {
            _adminToolsLogic.ClearCache(ctx.User.Id);
        }
    }
}
