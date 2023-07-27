using ModelManagerServer.St4.Enums;
using System.Linq;

namespace ModelManagerServer.Service
{
    public static class Extensions
    {
        public static List<(K, V)> ToList<K, V>(this IDictionary<K, V> map)
        {
            return map.Select(kvp => (kvp.Key, kvp.Value)).ToList();
        }

        public static Dictionary<K, V> ToDictionary<K, V>(this IEnumerable<KeyValuePair<K, V>> collection)
            where K : notnull
        {
            return collection.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static Dictionary<K, V> ToDictionary<K, V>(this IEnumerable<(K Key, V Value)> collection)
            where K : notnull, IComparable<K>
        {
            return collection.Distinct(
                new InlineEqualityComparer<(K, V)>((k1, k2) => k1.Item1.CompareTo(k2.Item1) == 0)
            ).ToDictionary(kvp => kvp.Item1, kvp => kvp.Item2);
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

        public static bool CanConvertModel(this St4PartState state) => state == St4PartState.Working;
    }

    public class InlineEqualityComparer<T> : IEqualityComparer<T>
    {
        public Func<T, T, bool> cmp { get; set; }

        public InlineEqualityComparer(Func<T, T, bool> cmp)
        {
            this.cmp = cmp;
        }
        public bool Equals(T x, T y)
        {
            return cmp(x, y);
        }

        public int GetHashCode(T obj)
        {
            return obj.GetHashCode();
        }

    }
}
