namespace BookStore.Common.Extensions;

public static class EnumerableExtensions
{
    public static void Each<T>(this IEnumerable<T> items, Action<int, T> @action)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        var allItems = items.ToList();
        for (var i = 0; i < allItems.Count; i++)
        {
            @action(i, allItems[i]);
        }
    }

    public static void EachWithIndex<T>(this IEnumerable<T> items, int index, Action<int, T> @action)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        var allItems = items.ToList();
        for (var i = 0; i < allItems.Count; i++)
        {
            @action(i + index, allItems[i]);
        }
    }

    public static void Each<T>(this IEnumerable<T> items, Action<T> @action) =>
        items.Each((i, item) => @action(item));

    public static async Task EachAsync<T>(this IEnumerable<T> items, Func<T, Task> func)
    {
        foreach (var item in items)
        {
            await func(item);
        }
    }
}

