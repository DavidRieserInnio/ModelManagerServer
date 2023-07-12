namespace ModelManagerServer.Service
{
    public static class Extensions
    {
        public static Dictionary<K, V> ToDictionary<K, V>(this ICollection<KeyValuePair<K, V>> collection)
            where K : notnull
        {
            return collection.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        public static IEnumerable<T>? CheckEmpty<T>(this IEnumerable<T> values)
        {
            return values.Any() ? values : null;
        }
    }
}
