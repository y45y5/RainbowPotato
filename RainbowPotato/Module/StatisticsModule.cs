using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using RainbowPotato.Model;
using RainbowPotato.Repositories;

namespace RainbowPotato.Module
{
    internal class StatisticsModule : BaseCommandModule
    {
        private readonly IMongoRepository<GuildConfigModel> guildConfigRepository;

        public StatisticsModule(IMongoRepository<GuildConfigModel> guildConfigRepository) 
        {
            this.guildConfigRepository = guildConfigRepository;
        }

        [Command("stats")]
        [RequireGuild]
        [Hidden]
        public async Task SendGuildStatistics(CommandContext ctx)
        {
            GuildConfigModel guildConfig = await guildConfigRepository.GetResults($"GuildConfig#{ctx.Guild.Id}");

            Console.WriteLine(guildConfig.guildId);
        }
    }
}
