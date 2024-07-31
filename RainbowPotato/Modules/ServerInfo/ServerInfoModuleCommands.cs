using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using RainbowPotato.Model;
using RainbowPotato.Repositories;

namespace RainbowPotato.Modules.ServerInfo
{
    internal class ServerInfoModuleCommands : BaseCommandModule
    {
        private readonly IMongoRepository<GuildConfigModel> _guildConfigRepository;
        private readonly ServerInfoModuleLogic statisticsModuleLogic = new();

        public ServerInfoModuleCommands(IMongoRepository<GuildConfigModel> guildConfigRepository)
        {
            _guildConfigRepository = guildConfigRepository;
        }

        [Command("info")]
        [RequireGuild]
        [Hidden]
        public async Task SendGuildStatistics(CommandContext ctx)
        {
            //GuildConfigModel guildConfig = await guildConfigRepository.GetResults($"GuildConfig#{ctx.Guild.Id}");

            await ctx.RespondAsync(statisticsModuleLogic.GetServerInformation(ctx.Guild));
        }
    }
}
