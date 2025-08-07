namespace TestFabric;

/// <summary>
///     Abstract class for comparing objects of type T.
/// </summary>
/// <typeparam name="T">The type of the objects to compare, which must be a reference type.</typeparam>
public abstract class ObjectEqualityComparer<T>(Action<string> writeLine = null) : IEqualityComparer<T>
    where T : class
{
    /// <summary>
    ///     Gets the action to write a line of text.
    /// </summary>
    protected Action<string> WriteLine { get; } = writeLine;

    /// <inheritdoc />
    public bool Equals(T x, T y)
    {
        if (x == null && y == null)
        {
            return true;
        }

        if (x != null && y != null)
        {
            return ReferenceEquals(x, y) || EqualsImpl(x, y);
        }

        WriteLine?.Invoke($"Only one of the items is null: {x == null} != {y == null}");
        return false;
    }

    /// <inheritdoc />
    public int GetHashCode(T obj)
    {
        return obj.GetHashCode();
    }

    /// <summary>
    ///     Abstract method to compare two objects of type T when they are both not null and a different reference.
    /// </summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>True if the objects are equal, otherwise false.</returns>
    protected abstract bool EqualsImpl(T x, T y);
}
