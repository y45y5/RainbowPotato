using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace RainbowPotato.Modules.AdminTools
{
    internal class DevToolsModuleCommands : BaseCommandModule
    {
        private readonly DevToolsModuleLogic _devToolsLogic;

        public DevToolsModuleCommands(DevToolsModuleLogic devToolsLogic)
        {
            _devToolsLogic = devToolsLogic;
        }

        [Command("clearcache"), Hidden]
        public async Task ClearCache(CommandContext ctx)
        {
            _devToolsLogic.ClearCache(ctx.User.Id);

            CustomUtils.AddSuccessReaction(ctx.Client, ctx.Message);
        }

        [Command("test"), Hidden]
        public async Task TestCommand(CommandContext ctx)
        {
            // Use it to test new functionalities

            // current test - add option to get all album hashes from database (triggers? create event hadler?)
            ImgurUtils imgurUtils = new ImgurUtils();
            await ctx.RespondAsync(await imgurUtils.GetRandomImgurAlbumResultAsEmbed("xHZTNsM"));
        }
    }
}
