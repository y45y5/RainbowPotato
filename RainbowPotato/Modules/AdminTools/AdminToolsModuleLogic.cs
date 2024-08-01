using RainbowPotato.Model;
using RainbowPotato.Repositories;

namespace RainbowPotato.Modules.AdminTools
{
    internal class AdminToolsModuleLogic
    {
        private readonly IMongoRepository<GuildConfigModel> _guildConfigRepository;

        public AdminToolsModuleLogic(IMongoRepository<GuildConfigModel> guildConfigRepository)
        {
            _guildConfigRepository = guildConfigRepository;
        }

        public void ClearCache(ulong memberId)
        {
            if (!Tools.IsDeveloper(memberId)){
                return;
            }

            _guildConfigRepository.ClearCache();
        }
    }
}
