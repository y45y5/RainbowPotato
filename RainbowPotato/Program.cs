using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using RainbowPotato.Cache;
using RainbowPotato.Dao;
using RainbowPotato.Model;
using RainbowPotato.Modules.ServerInfo;
using RainbowPotato.Repositories;

namespace RainbowPotato
{
    class Program
    {
        static void Main()
        {
            BotSetUpAsync().GetAwaiter().GetResult();
        }

        static async Task BotSetUpAsync()
        {
            DiscordClient discordClient = new DiscordClient(new DiscordConfiguration()
            {
                Token = Settings.botToken,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All
            });

            IServiceCollection services = new ServiceCollection();
            ConfigureRepositories(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            CommandsNextExtension commandsNextConfiguration = discordClient.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { ">" },
                Services = serviceProvider
            });

            SlashCommandsExtension slashCommands = discordClient.UseSlashCommands();
            slashCommands.RegisterCommands<ServerInfoModuleSlash>();

            commandsNextConfiguration.RegisterCommands<ServerInfoModuleCommands>();

            await discordClient.ConnectAsync(new DiscordActivity("Ziemniak, a nawet batat", ActivityType.Playing));
            await Task.Delay(-1);
        }

        public static void ConfigureRepositories(IServiceCollection services)
        {
            services.AddSingleton<IDao<GuildConfigModel>, Dao<GuildConfigModel>>();
            services.AddSingleton<ICustomCache<GuildConfigModel>, CustomCache<GuildConfigModel>>();
            services.AddSingleton<IMongoRepository<GuildConfigModel>, MongoRepository<GuildConfigModel>>();
        }
    }
}