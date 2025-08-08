using System.Runtime.CompilerServices;

namespace TestFabric.Data;

public abstract class RandomNumberFactory<T>(
    T smallest,
    T small,
    T normal,
    T large,
    int? seed = null)
    : IRandomNumberFactory<T>
{
    private readonly Random _random = seed.ToRandom();
    private readonly object _syncLock = new();

    /// <inheritdoc />
    public T Create()
    {
        return Create(small, normal);
    }

    /// <inheritdoc />
    public T CreateSmall()
    {
        return Create(smallest, small);
    }

    /// <inheritdoc />
    public T CreateLarge()
    {
        return Create(normal, large);
    }

    /// <inheritdoc />
    public T CreatePositive()
    {
        return CreatePositive(small, normal);
    }

    /// <inheritdoc />
    public T CreateSmallPositive()
    {
        return CreatePositive(smallest, small);
    }

    /// <inheritdoc />
    public T CreateLargePositive()
    {
        return CreatePositive(normal, large);
    }

    /// <inheritdoc />
    public T CreateStrictlyPositive()
    {
        return CreateStrictlyPositive(small, normal);
    }

    /// <inheritdoc />
    public T CreateSmallStrictlyPositive()
    {
        return CreateStrictlyPositive(smallest, small);
    }

    /// <inheritdoc />
    public T CreateLargeStrictlyPositive()
    {
        return CreateStrictlyPositive(normal, large);
    }

    /// <summary>
    ///     Creates a random number within the specified bounds.
    /// </summary>
    /// <param name="minInclusive">
    ///     The minimum value of the range (inclusive).
    /// </param>
    /// <param name="maxInclusive">
    ///     The maximum value of the range (inclusive).
    /// </param>
    /// <returns>
    ///     A random number of type T.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int CreateRandom(int minInclusive, int maxInclusive)
    {
        lock (_syncLock)
        {
            return _random.Next(minInclusive, maxInclusive + 1);
        }
    }

    /// <summary>
    ///     Creates a random sign (-1 or 1).
    /// </summary>
    /// <returns>
    ///     A random sign of type int.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int CreateSign()
    {
        lock (_syncLock)
        {
            return _random.Next(0, 2) == 0 ? 1 : -1;
        }
    }

    /// <summary>
    ///     Creates a random number within the specified bounds.
    /// </summary>
    /// <returns>
    ///     A random number of type T.
    /// </returns>
    protected abstract T Create(T lowerBound, T upperBound);

    /// <summary>
    ///     Creates a random positive number within the specified bounds.
    /// </summary>
    /// <returns>
    ///     A random positive number of type T.
    /// </returns>
    protected abstract T CreatePositive(T lowerBound, T upperBound);

    private T CreateStrictlyPositive(T lowerBound, T upperBound)
    {
        var strictLowerBound = BoundToSmallestPositive(lowerBound);
        return CreatePositive(strictLowerBound, upperBound);
    }

    /// <summary>
    ///     Binds the specified lower bound to the smallest positive value.
    /// </summary>
    /// <param name="lowerBound">The lower bound.</param>
    /// <returns>The bound to the smallest positive value.</returns>
    protected abstract T BoundToSmallestPositive(T lowerBound);
}
