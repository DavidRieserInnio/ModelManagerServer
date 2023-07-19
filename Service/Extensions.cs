using System.Linq;

namespace ModelManagerServer.Service
{
    public static class Extensions
    {
        public static Dictionary<K, V> ToDictionary<K, V>(this IEnumerable<KeyValuePair<K, V>> collection)
            where K : notnull
        {
            return collection.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static Dictionary<K, V> ToDictionary<K, V>(this IEnumerable<(K Key, V Value)> collection)
            where K : notnull
        {
            return collection.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static Dictionary<string, string> ToDictionary(this IFormCollection formCollection)
        {
            return formCollection.SelectMany(formKeyValue =>
                formKeyValue.Value.Count switch
                {
                    0 => Array.Empty<(string, string)>(),
                    1 => new (string, string)[1] { (formKeyValue.Key, formKeyValue.Value[0]!) },
                    _ => formKeyValue.Value.Select((v, i) => 
                        (CreateIndexedKey(formKeyValue.Key[..^2], i), v!)
                    )
                }
            )
            .ToDictionary();

            static string CreateIndexedKey(string key, int idx) => $"{key}[{idx}]";
        }

        public static ILookup<string, string> ToLookup(this IFormCollection formCollection)
        {
            return formCollection.SelectMany(formKeyValue =>
                formKeyValue.Value.Count switch
                {
                    0 => Array.Empty<(string, string)>(),
                    1 => new (string, string)[1] { (formKeyValue.Key, formKeyValue.Value[0]!) },
                    _ => formKeyValue.Value.Select((v, i) => (formKeyValue.Key[..^2], v!))
                }
            )
            .ToLookup(item => item.Item1, item => item.Item2);
        }

        public static IEnumerable<T>? CheckEmpty<T>(this IEnumerable<T> values)
        {
            return values.Any() ? values : null;
        }
    }
}
