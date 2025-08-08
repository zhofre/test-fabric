namespace TestFabric.Data;

/// <summary>
///     Represents a range between two values, inclusive the start value, exclusive the end value.
/// </summary>
/// <typeparam name="T">The type of the elements in the range. Must implement IComparable{T}.</typeparam>
public class NumberRange<T>(T start, T end) : IRange<T>
    where T : IComparable<T>
{
    /// <summary>
    ///     Inclusive start of the range.
    /// </summary>
    public T Start { get; } = start;

    /// <summary>
    ///     Exclusive end of the range.
    /// </summary>
    public T End { get; } = end;
}
