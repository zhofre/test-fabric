namespace TestFabric;

/// <summary>
///     Compares two lists of type T based on their items using the provided item comparer.
/// </summary>
/// <typeparam name="T">The type of the elements in the lists.</typeparam>
public class ListEqualityComparer<T>(
    IEqualityComparer<T> itemComparer = null,
    Action<string> writeLine = null)
    : ObjectEqualityComparer<List<T>>(writeLine)
{
    private readonly IEqualityComparer<T> _itemComparer = itemComparer
                                                          ?? EqualityComparer<T>.Default;

    /// <inheritdoc />
    protected override bool EqualsImpl(List<T> x, List<T> y)
    {
        if (x.Count != y.Count)
        {
            WriteLine?.Invoke($"Count is different: {x.Count} != {y.Count}");
            return false;
        }

        for (var i = 0; i < x.Count; i++)
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
