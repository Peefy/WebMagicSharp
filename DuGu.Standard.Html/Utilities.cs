
using System;
using System.Collections.Generic;

namespace DuGu.Standard.Html
{
    internal static class Utilities
    {
        public static TValue GetDictionaryValueOrDefault<TKey, TValue>(Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue)) where TKey : class
        {
            TValue value;
            if (!dict.TryGetValue(key, out value))
                return defaultValue;
            return value;
        }
    }
}