namespace TestFabric;

/// <summary>
///     Contains various methods for comparing objects and collections with different criteria.
///     Provides specialized comparers for doubles, arrays, and lists.
/// </summary>
public static class Compare
{
    /// <summary>
    ///     Creates a comparer for doubles that considers two values equal if their absolute difference is less than the
    ///     specified precision.
    /// </summary>
    /// <param name="precision">The maximum allowed difference between two double values for them to be considered equal.</param>
    /// <returns>
    ///     An IEqualityComparer&lt;double&gt; that can be used to compare double values based on the absolute difference
    ///     criteria.
    /// </returns>
    public static IEqualityComparer<double> DoubleAbsolute(double precision)
    {
        return new AbsoluteDoubleComparer(precision);
    }

    /// <summary>
    ///     Creates a comparer for doubles that considers two values equal if their difference is within the specified relative
    ///     and absolute tolerances.
    /// </summary>
    /// <param name="relativeTolerance">
    ///     The maximum allowed difference as a fraction of the larger out of the two double
    ///     values.
    /// </param>
    /// <param name="absoluteTolerance">
    ///     The minimum allowed difference between two double values for them to be considered
    ///     equal when close to zero.
    /// </param>
    /// <returns>
    ///     An IEqualityComparer&lt;double&gt; that can be used to compare double values based on the relative and absolute
    ///     tolerance criteria.
    /// </returns>
    public static IEqualityComparer<double> DoubleRelative(double relativeTolerance, double absoluteTolerance)
    {
        return new RelativeDoubleComparer(relativeTolerance, absoluteTolerance);
    }

    /// <summary>
    ///     Creates a comparer for arrays that considers two arrays equal if all corresponding elements are equal based on the
    ///     specified item comparer.
    /// </summary>
    /// <typeparam name="T">The type of elements in the arrays to compare.</typeparam>
    /// <param name="itemComparer">
    ///     The IEqualityComparer used to compare individual items within the arrays. If null,
    ///     uses default equality.
    /// </param>
    /// <param name="writeLine">
    ///     Action for logging or other output during comparison. Can be null if no additional output is
    ///     required.
    /// </param>
    /// <returns>
    ///     An IEqualityComparer&lt;T[]&gt; that can be used to compare arrays based on the item comparer criteria.
    /// </returns>
    public static IEqualityComparer<T[]> Array<T>(
        IEqualityComparer<T> itemComparer = null,
        Action<string> writeLine = null)
    {
        return new ArrayEqualityComparer<T>(itemComparer, writeLine);
    }

    /// <summary>
    ///     Creates a comparer for lists that considers two lists equal if they contain the same elements in the same order,
    ///     using the provided item comparer.
    /// </summary>
    /// <typeparam name="T">The type of the elements in the lists.</typeparam>
    /// <param name="itemComparer">
    ///     The IEqualityComparer to use for comparing individual items. If null, uses default equality
    ///     comparer.
    /// </param>
    /// <param name="writeLine">Action to perform when logging information. If null, no logging occurs.</param>
    /// <returns>
    ///     An IEqualityComparer&lt;List&lt;T&gt;&gt; that can be used to compare lists of type T based on the item comparer
    ///     criteria.
    /// </returns>
    public static IEqualityComparer<List<T>> List<T>(
        IEqualityComparer<T> itemComparer = null,
        Action<string> writeLine = null)
    {
        return new ListEqualityComparer<T>(itemComparer, writeLine);
    }
}
