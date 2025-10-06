using Microsoft.Extensions.Caching.Memory;

namespace Services.Auth.Infrastructure.Cache

{
    public class CacheManager
    {
        private static IMemoryCache Cache;

        public static void Initialize(IMemoryCache cache)
        {
            Cache = cache;
        }

        public static void SetValue<T>(string key, T value, int ExpirationInSeconds)
        {
            Cache.Set(key, value, TimeSpan.FromSeconds(ExpirationInSeconds));
        }

        public static void Increment(string key, int ExpirationInSeconds)
        {
            if (ContainsKey(key))
            {
                int previousValue = GetValue<int>(key);
                SetValue<int>(key, previousValue + 1, ExpirationInSeconds);
            }
            else
                Cache.Set(key, 1, TimeSpan.FromSeconds(ExpirationInSeconds));
        }

        public static bool ContainsKey(string key)
        {
            if (Cache.TryGetValue(key, out var value))
                return true;
            return false;
        }

        public static T? GetValue<T>(string key)
        {
            if (Cache.TryGetValue(key, out T value))
                return value;

            return default;
        }

        public static void RemoveValue(string key)
        {
            Cache.Remove(key);
        }

    }
}
