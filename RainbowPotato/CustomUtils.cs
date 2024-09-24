using DSharpPlus;
using DSharpPlus.Entities;

namespace RainbowPotato
{
    internal static class CustomUtils
    {
        public static ulong GetGuildIdFromCacheKey(string cacheKey)
        {
            return ulong.Parse(cacheKey[(cacheKey.IndexOf("#") + 1)..]);
        }

        public static string GetCollectionNameFromCacheKey(string cacheKey)
        {
            return cacheKey[..cacheKey.IndexOf("#")];
        }

        public static bool IsDeveloper(ulong memberId)
        {
            return memberId == 412611817425207307;
        }

        public static string ReadToken(string path)
        {
            IEnumerable<string> lines = File.ReadLines(path);
            foreach (string line in lines)
            {
                return line;
            }

            return String.Empty;
        }

        public async static void AddProcessingReaction(DiscordClient client, DiscordMessage message)
        {
            await message.CreateReactionAsync(DiscordEmoji.FromName(client, ":Processing:"));
        }

        public async static void AddSuccessReaction(DiscordClient client, DiscordMessage message)
        {
            await message.CreateReactionAsync(DiscordEmoji.FromName(client, ":Success:"));
        }

        public async static void AddFailedReaction(DiscordClient client, DiscordMessage message)
        {
            await message.CreateReactionAsync(DiscordEmoji.FromName(client, ":Failed:"));
        }

        public async static void ClearAllReactions(DiscordMessage message)
        {
            await message.DeleteAllReactionsAsync();
        }
    }
}
