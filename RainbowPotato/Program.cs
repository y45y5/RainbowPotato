using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlus.Interactivity.Extensions;
using Microsoft.Extensions.DependencyInjection;
using RainbowPotato.Model;
using RainbowPotato.Module;
using RainbowPotato.Repositories;

namespace RainbowPotato
{
    class Program
    {
        static void Main(string[] args)
        {
            BotSetUpAsync().GetAwaiter().GetResult();
        }

        static async Task BotSetUpAsync()
        {
            DiscordClient discordClient = new DiscordClient(new DiscordConfiguration()
            {
                Token = Variables.botToken,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.All
            });

            CommandsNextExtension commandsNextConfiguration = discordClient.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new[] { ">" }
            });

            discordClient.UseInteractivity(new InteractivityConfiguration()
            {
                PollBehaviour = PollBehaviour.KeepEmojis,
                Timeout = TimeSpan.FromSeconds(30)
            });

            // Register Repositories as singletons in ServiceCollection object
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<IMongoRepository<GuildConfigModel>, MongoRepository<GuildConfigModel>>();

            // Register commands modules
            commandsNextConfiguration.RegisterCommands<StatisticsModule>();

            await discordClient.ConnectAsync(new DiscordActivity("Ziemniak, a nawet batat....", ActivityType.Playing));
            await Task.Delay(-1);
        }
    }
}