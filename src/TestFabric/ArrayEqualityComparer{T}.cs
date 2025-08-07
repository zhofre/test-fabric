namespace TestFabric;

/// <summary>
///     Compares two arrays of type T using the provided item comparer.
/// </summary>
/// <typeparam name="T">The type of elements in the arrays to compare.</typeparam>
public class ArrayEqualityComparer<T>(
    IEqualityComparer<T> itemComparer = null,
    Action<string> writeLine = null)
    : ObjectEqualityComparer<T[]>(writeLine)
{
    private readonly IEqualityComparer<T> _itemComparer = itemComparer
                                                          ?? EqualityComparer<T>.Default;

    /// <inheritdoc />
    protected override bool EqualsImpl(T[] x, T[] y)
    {
        if (x.Length != y.Length)
        {
            WriteLine?.Invoke($"Length is different: {x.Length} != {y.Length}");
            return false;
        }

        for (var i = 0; i < x.Length; i++)
        {
            if (_itemComparer.Equals(x[i], y[i]))
            {
                continue;
            }

            WriteLine?.Invoke($"Items at index {i} are not equal");
            return false;
        }

        return true;
    }
}
