using System;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;
public static class CollectionExt
{
    static readonly Random _rnd;
    static CollectionExt()
    {
        _rnd = new Random(DateTime.Now.Millisecond);
    }
    /// <summary>
    /// Shuffles the contents of the list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> list, Random random = null)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = (random ?? _rnd).Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    /// <summary>
    /// Gets the element at i, where i will by wrapped. Useful when cycling trough lists.
    /// </summary>
    /// <returns></returns>
    public static T ElementAtCycled<T>(this IList<T> list, int i)
    {
        var count = list.Count; // cache because count may involve enumerting the collection
        if (count == 0) return default(T); // TODO exception, assert or default?
        if (count == 1) return list[0];
        i = ((i % count) + count) % count;
        return list[i];
    }
    /// <summary>
    /// Gets the element at i, where i will by clamped. Useful when cycling trough lists.
    /// </summary>
    /// <returns></returns>
    public static T ElementAtClamped<T>(this IList<T> list, int i)
    {
        var count = list.Count; // cache because count may involve enumerating the collection
        if (count == 0) throw new IndexOutOfRangeException();
        i = Math.Max(0, Math.Min(i, list.Count - 1));
        return list[i];
    }
    /// <summary>
    /// Picks a random element from the list and returns it.
    /// </summary>
    public static T GetRandomElement<T>(this IList<T> list)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (list.Count == 0) throw new ArgumentOutOfRangeException(nameof(list), "List must have at least one element");
        var n = list.Count;
        return n > 0 ? list[_rnd.Next(n)] : default(T);
    }
    /// <summary>
    ///
    /// </summary>
    public static int RandomIndex<T>(this IList<T> list)
    {
        var n = list.Count;
        return _rnd.Next(n);
    }
    /// <summary>
    ///
    /// </summary>
    public static int WrapIndex<T>(this IList<T> list, int i)
    {
        var n = list.Count;
        return ((i % n) + n) % n;
    }
    /// <summary>
    ///
    /// </summary>
    public static void EnqueueAll<T>(this Queue<T> queue, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            queue.Enqueue(item);
        }
    }
    /// <summary>
    /// Returns true is the string is null or contains only whitespace
    /// </summary>
    public static bool IsNullOrWhiteSpace(this string value)
    {
        return string.IsNullOrWhiteSpace(value);
    }
    /// <summary>
    /// Returns true if the collection is null or doesn't have any elements
    /// </summary>
    public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
    {
        return collection == null || collection.Any() == false;
    }
    public static IEnumerable<T> EmptyWhenNull<T>(this IEnumerable<T> collection)
    {
        return collection ?? Enumerable.Empty<T>();
    }
    public static string Format<T>(this IEnumerable<T> collection, string separator = ",", string itemFormat = "{0}", string empty = "<empty>")
    {
        var items = collection.Select(t => string.Format(itemFormat, t)).ToArray();
        return items.Length == 0 ? empty : string.Join(separator, items);
    }
    /// <summary>
    /// Returns a nicely formatted string of the elements in the collection.
    /// </summary>
    public static string Format<TKey, TValue>(this Dictionary<TKey, TValue> collection, string separator = ", ", string keyValueFormat = "{0}: {1}")
    {
        var items = collection.Select(t => string.Format(keyValueFormat, t.Key, t.Value)).ToArray();
        return string.Join(separator, items);
    }
    /// <summary>
    /// Returns a formatted string with type and element information
    /// </summary>
    public static string ToDebugString<T>(this IEnumerable<T> collection)
    {
        var items = collection.Select(t => t.ToString()).ToArray();
        return typeof(T).Name + "[" + items.Length + "] {" + string.Join(", ", items) + "}";
    }
    /// <summary>
    ///
    /// </summary>
    public static bool NotNullAndContainsKey<TKey, T>(this Dictionary<TKey, T> dic, TKey key) where TKey : class
    {
        return dic != null && dic.ContainsKey(key);
    }
    /// <summary>
    ///
    /// </summary>
    public static T PeekFirst<T>(this LinkedList<T> list)
    {
        return list.First.Value;
    }
    /// <summary>
    ///
    /// </summary>
    public static T PeekLast<T>(this LinkedList<T> list)
    {
        return list.Last.Value;
    }
    /// <summary>
    ///
    /// </summary>
    public static T PopFirst<T>(this LinkedList<T> list)
    {
        var f = list.First.Value;
        list.RemoveFirst();
        return f;
    }
    /// <summary>
    ///
    /// </summary>
    public static T PopLast<T>(this LinkedList<T> list)
    {
        var f = list.Last.Value;
        list.RemoveLast();
        return f;
    }
    public static TValue GetKeyOrFallback<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue fallback)
    {
        TValue value;
        return dictionary.TryGetValue(key, out value)
                   ? value
                   : fallback;
    }
}