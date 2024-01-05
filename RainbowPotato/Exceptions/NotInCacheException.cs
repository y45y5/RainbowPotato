namespace RainbowPotato.Exceptions
{
    internal class NotInCacheException : Exception
    {
        public NotInCacheException() { }

        public NotInCacheException(string message) : base(message) { }

        public NotInCacheException(string message, Exception innerException) : base(message, innerException) { }
    }
}
