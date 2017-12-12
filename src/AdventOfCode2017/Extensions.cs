using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2017
{
    public static class Extensions
    {
        public static string[] SplitOn(this string source, params char[] separators)
        {
            return source.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> SplitLines(this string source)
        {
            return source.SplitOn('\n').TrimEach();
        }

        public static IEnumerable<string> TrimEach(this IEnumerable<string> source)
        {
            return source.Select(x => x.Trim());
        }

        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key,
            TValue defaultVaue = default(TValue))
        {
            return dict != null && dict.TryGetValue(key, out var value) ? value : defaultVaue;
        }

        public static bool HasAny<T>(this IEnumerable<T> source)
        {
            return source != null && source.Any();
        }

        public static string AsString(this IEnumerable<char> chars)
        {
            return chars.HasAny() ? new string(chars.ToArray()) : string.Empty;
        }

        public static string OrderedString(this string source)
        {
            return source == null
                ? string.Empty
                : source.OrderBy(c => c).AsString();
        }

        public static (int index, int? value) MaxOf(this IList<int> source)
        {
            if (!source.HasAny())
                return (-1, null);

            var index = 0;
            var value = source[0];
            for(var k = 1; k < source.Count; ++k)
            {
                if (source[k] > value)
                {
                    value = source[k];
                    index = k;
                }
            }
            return (index, value);
        }

        public static string JoinStrings(this IEnumerable<object> source, string separator = "")
        {
            return source.HasAny()
                ? string.Join(separator, source)
                : string.Empty;
        }
    }
}
