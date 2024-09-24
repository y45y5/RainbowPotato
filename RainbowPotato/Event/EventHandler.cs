using DSharpPlus;
using Microsoft.Extensions.DependencyInjection;
using RainbowPotato.Model;
using RainbowPotato.Modules.Imgur;
using RainbowPotato.Repositories;

namespace RainbowPotato.Event
{
    internal class EventHandler
    {
        public DiscordClient AddEventHandlers(DiscordClient client, IServiceProvider serviceProvider)
        {
            ISimpleMongoRepository<ImgurAlbumTriggerModel> imgurMongoRepository = serviceProvider.GetService<ISimpleMongoRepository<ImgurAlbumTriggerModel>>();

            ImgurModuleEvents imgurModuleEvents = new ImgurModuleEvents(imgurMongoRepository);

            client.MessageCreated += async (client, @event) =>
            {
                if (!@event.Author.IsBot && @event.Channel.Guild != null)
                {
                    imgurModuleEvents.CheckForImgurTriggers(@event);
                }
            };

            return client;
        }
    }
}
