using DSharpPlus.EventArgs;
using RainbowPotato.Model;
using RainbowPotato.Repositories;

namespace RainbowPotato.Modules.Imgur
{
    internal class ImgurModuleEvents
    {
        private readonly ISimpleMongoRepository<ImgurAlbumTriggerModel> _imgurAlbumTriggerRepository;
        private Dictionary<string, string> _imgurTriggersDict = new Dictionary<string, string>();
        private ImgurUtils _imgurUtils = new ImgurUtils();

        public ImgurModuleEvents(ISimpleMongoRepository<ImgurAlbumTriggerModel> imgurAlbumTriggerRepository)
        {
            _imgurAlbumTriggerRepository = imgurAlbumTriggerRepository;
        }

        public async void CheckForImgurTriggers(MessageCreateEventArgs @event)
        {
            if (_imgurTriggersDict.Count < 1)
            {
                LoadTriggersFromDatabase();
            }

            if (_imgurTriggersDict.ContainsKey(@event.Message.Content))
            {
                await @event.Channel.SendMessageAsync(await _imgurUtils.GetRandomImgurAlbumResultAsEmbed(_imgurTriggersDict[@event.Message.Content]));
                await @event.Message.DeleteAsync();
            }
        }

        private async void LoadTriggersFromDatabase()
        {
            foreach (ImgurAlbumTriggerModel imgurAlbumTriggerModel in await _imgurAlbumTriggerRepository.GetResults("ImgurAlbumTrigger#0"))
            {
                _imgurTriggersDict[imgurAlbumTriggerModel.trigger] = imgurAlbumTriggerModel.albumHash;
            }
        }
    }
}
