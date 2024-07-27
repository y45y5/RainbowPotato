namespace RainbowPotato
{
    internal static class Tools
    {
        public static ulong GetGuildIdFromCacheKey(string cacheKey)
        {
            return ulong.Parse(cacheKey[(cacheKey.IndexOf("#") + 1)..]);
        }

        public static string GetCollectionNameFromCacheKey(string cacheKey)
        {
            return cacheKey[..cacheKey.IndexOf("#")];
        }
    }
}
