using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.DependencyInjection;
using RainbowPotato.Cache;
using RainbowPotato.Dao;
using RainbowPotato.Model;
using RainbowPotato.Modules.AdminTools;
using RainbowPotato.Modules.GuildInfo;
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
            bool dev = true;

            if (dev)
            {
                Settings.botToken = CustomUtils.ReadToken("B:/VisualProjects/RainbowPotatoToken.txt");
                Settings.mongoClientString = CustomUtils.ReadToken("B:/VisualProjects/MongoAccess.txt");
            }
            else
            {
                Settings.botToken = CustomUtils.ReadToken("/home/ec2-user/RainbowPotatoToken.txt");
                Settings.mongoClientString = CustomUtils.ReadToken("/home/ec2-user/MongoAccess.txt");
            }

            DiscordClient discordClient = new DiscordClient(new DiscordConfiguration()
            {
                Token = Settings.botToken,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All
            });

            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            CommandsNextExtension commandsNextConfiguration = discordClient.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { "!" },
                Services = serviceProvider
            });

            commandsNextConfiguration.RegisterCommands<DevToolsModuleCommands>();
            commandsNextConfiguration.RegisterCommands<GuildInfoModuleCommands>();

            SlashCommandsExtension slashCommands = discordClient.UseSlashCommands(new SlashCommandsConfiguration(){
                Services = serviceProvider
            });

            slashCommands.RegisterCommands<GuildInfoModuleSlash>();

            await discordClient.ConnectAsync(new DiscordActivity("Ziemniak, a nawet batat", ActivityType.Playing));
            await Task.Delay(-1);
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDao<GuildConfigModel>, Dao<GuildConfigModel>>();
            services.AddSingleton<ICustomCache<GuildConfigModel>, CustomCache<GuildConfigModel>>();
            services.AddSingleton<IMongoRepository<GuildConfigModel>, MongoRepository<GuildConfigModel>>();

            services.AddSingleton<DevToolsModuleLogic>();
            services.AddSingleton<GuildInfoModuleLogic>();
        }
    }
}