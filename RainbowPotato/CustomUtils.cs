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
    }
}
