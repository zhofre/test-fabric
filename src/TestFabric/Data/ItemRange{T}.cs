namespace TestFabric.Data;

/// <summary>
///     Represents a range of items.
/// </summary>
/// <typeparam name="T">The type of items in the range.</typeparam>
public class ItemRange<T>(params IEnumerable<T> items) : IRange<T>
{
    /// <summary>
    ///     Represents the collection of items within an ItemRange.
    /// </summary>
    public T[] Items { get; } = items.ToArray();
}
