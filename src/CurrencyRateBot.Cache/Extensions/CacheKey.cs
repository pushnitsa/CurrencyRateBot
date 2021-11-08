using System;

namespace CurrencyRateBot.Cache.Extensions
{
    public static class CacheKey
    {
        public static string GetKey(params string[] keyparts)
        {
            return string.Join("-", keyparts);
        }

        public static string GetKey(Type type, params string[] keyparts)
        {
            return string.Join("-", type.Name, GetKey(keyparts));
        }
    }
}
