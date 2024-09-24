using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using RainbowPotato.Model;
using RainbowPotato.Modules.Imgur;
using RainbowPotato.Repositories;

namespace RainbowPotato.Event
{
    internal class EventHandler
    {
        public DiscordClient AddEventHandlers(DiscordClient client, IServiceCollection services)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ISimpleMongoRepository<ImgurAlbumTriggerModel> imgurMongoRepository = serviceProvider.GetService<ISimpleMongoRepository<ImgurAlbumTriggerModel>>();

            ImgurModuleEvents imgurModuleEvents = new ImgurModuleEvents(imgurMongoRepository);

            client.MessageCreated += (client, @event) =>
            {
                _ = Task.Run((Func<Task>)(async () =>
                {
                    if (!@event.Author.IsBot && @event.Channel.Guild != null)
                    {
                        imgurModuleEvents.CheckForImgurTriggers(@event);
                    }
                }));

                return Task.CompletedTask;
            };

            return client;
        }
    }
}
