namespace TestFabric.Data;

/// <summary>
///     Defines a contract for picking an element from a specified range.
/// </summary>
/// <typeparam name="T">The type of the elements within the range.</typeparam>
public interface IPicker<T> : IPicker
{
    /// <summary>
    ///     Selects an element from a specified range.
    /// </summary>
    /// <param name="range">The range from which to pick an element.</param>
    /// <returns>The selected element from the range.</returns>
    T Pick(IRange<T> range);
}
