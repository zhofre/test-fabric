namespace TestFabric.Data;

/// <summary>
///     Abstract class for picking an element from a specified range.
///     The type parameter TRange must implement the IRange&lt;T&gt; interface.
/// </summary>
/// <typeparam name="T">The type of the elements within the range.</typeparam>
/// <typeparam name="TRange">The type of the range, which must implement IRange&lt;T&gt;.</typeparam>
public abstract class RangePicker<T, TRange>(int? seed = null) : IPicker<T>
    where TRange : IRange<T>
{
    /// <summary>
    ///     Provides methods for generating random numbers.
    ///     This implementation is used internally by the RangePicker class to select elements from a range.
    /// </summary>
    protected readonly Random Random = seed.HasValue
        ? new Random(seed.Value)
        : new Random();

    /// <summary>
    ///     Selects an element from the specified range.
    /// </summary>
    /// <param name="range">The range from which to pick an element.</param>
    /// <returns>The selected element from the range.</returns>
    public T Pick(IRange<T> range)
    {
        return PickFromRange((TRange)range);
    }

    /// <summary>
    ///     Selects an element from the specified range.
    /// </summary>
    /// <param name="range">The range from which to pick an element.</param>
    /// <returns>The selected element from the range.</returns>
    protected abstract T PickFromRange(TRange range);
}
