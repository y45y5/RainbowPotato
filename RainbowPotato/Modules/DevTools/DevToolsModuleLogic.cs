﻿using RainbowPotato.Model;
using RainbowPotato.Repositories;

namespace RainbowPotato.Modules.AdminTools
{
    internal class DevToolsModuleLogic
    {
        private readonly IMongoRepository<GuildConfigModel> _guildConfigRepository;

        public DevToolsModuleLogic(IMongoRepository<GuildConfigModel> guildConfigRepository)
        {
            _guildConfigRepository = guildConfigRepository;
        }

        public void ClearCache(ulong memberId)
        {
            if (!CustomUtils.IsDeveloper(memberId))
            {
                return;
            }

            _guildConfigRepository.ClearCache();
        }
    }
}
