namespace TestFabric;

/// <summary>
///     Builder class for creating mock instances of <see cref="IEqualityComparer{T}" />.
///     Allows configuring the behavior of the <c>Equals</c> and <c>GetHashCode</c> methods.
/// </summary>
/// <typeparam name="T">The type that this equality comparer will operate on.</typeparam>
public class EqualityComparerMockBuilder<T> : MockBuilder<IEqualityComparer<T>>
{
    /// <summary>
    ///     Configures the mock to return a specific value when the Equals method is called.
    /// </summary>
    /// <param name="result">The value to return from the Equals method.</param>
    /// <returns>The current builder instance for method chaining.</returns>
    public EqualityComparerMockBuilder<T> WithEquals(bool result)
    {
        WithFunction(
            x => x.Equals(It.IsAny<T>(), It.IsAny<T>()), result);
        return this;
    }

    /// <summary>
    ///     Configures the mock to return a specific result when the Equals method is called.
    /// </summary>
    /// <param name="delegate">The delegate to execute from the Equals method.</param>
    /// <returns>The current builder instance for method chaining.</returns>
    public EqualityComparerMockBuilder<T> WithEquals(Func<T, T, bool> @delegate)
    {
        WithFunction(
            x => x.Equals(It.IsAny<T>(), It.IsAny<T>()),
            @delegate);
        return this;
    }

    /// <summary>
    ///     Configures the mock to return a specific value when the GetHashCode method is called.
    /// </summary>
    /// <param name="result">The value to return from the GetHashCode method.</param>
    /// <returns>The current builder instance for method chaining.</returns>
    public EqualityComparerMockBuilder<T> WithGetHashCode(int result)
    {
        WithFunction(
            x => x.GetHashCode(It.IsAny<T>()),
            result);
        return this;
    }

    /// <summary>
    ///     Configures the mock to return a specific value when the GetHashCode method is called.
    /// </summary>
    /// <param name="delegate">The delegate to execute from the GetHashCode method.</param>
    /// <returns>The current builder instance for method chaining.</returns>
    public EqualityComparerMockBuilder<T> WithGetHashCode(Func<T, int> @delegate)
    {
        WithFunction(
            x => x.GetHashCode(It.IsAny<T>()),
            @delegate);
        return this;
    }
}
