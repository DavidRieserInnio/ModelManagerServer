using System.Linq;

namespace ModelManagerServer.Service
{
    public static class Extensions
    {
        public static Dictionary<K, V> ToDictionary<K, V>(this ICollection<KeyValuePair<K, V>> collection)
            where K : notnull
        {
            return collection.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static Dictionary<string, string> ToDictionary(this IFormCollection formCollection)
        {
            return formCollection.SelectMany(s => {
                if (s.Value.Count == 0)
                {
                    return Array.Empty<(string, string)>();
                } 
                else if (s.Value.Count == 1)
                {
                    return new (string, string)[1] { (s.Key, s.Value[0]!) };
                } 
                else
                {
                    return s.Value.Select((v, i) => ($"{s.Key[..^2]}[{i}]", v!));
                }
            })
            .ToDictionary(kvp => kvp.Item1, kvp => kvp.Item2);
        }

        public static IEnumerable<T>? CheckEmpty<T>(this IEnumerable<T> values)
        {
            return values.Any() ? values : null;
        }
    }
}
