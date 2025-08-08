using System.Runtime.CompilerServices;

namespace TestFabric.Data;

public abstract class RandomNumberFactory<T>(
    T zero,
    T epsilon,
    T small,
    T medium,
    T large,
    int? seed = null)
    : IRandomNumberFactory<T>
{
    private readonly Random _random = seed.ToRandom();
    private readonly object _syncLock = new();

    /// <inheritdoc />
    public T Create()
    {
        return Create(zero, medium);
    }

    /// <inheritdoc />
    public T CreateSmall()
    {
        return Create(zero, small);
    }

    public T CreateMedium()
    {
        return Create(small, medium);
    }

    /// <inheritdoc />
    public T CreateLarge()
    {
        return Create(medium, large);
    }

    /// <inheritdoc />
    public T CreatePositive()
    {
        return CreatePositive(small, medium);
    }

    /// <inheritdoc />
    public T CreateSmallPositive()
    {
        return CreatePositive(zero, small);
    }

    public T CreateMediumPositive()
    {
        return CreatePositive(small, medium);
    }

    /// <inheritdoc />
    public T CreateLargePositive()
    {
        return CreatePositive(medium, large);
    }

    /// <inheritdoc />
    public T CreateStrictlyPositive()
    {
        return CreatePositive(small, medium);
    }

    /// <inheritdoc />
    public T CreateSmallStrictlyPositive()
    {
        return CreatePositive(epsilon, small);
    }

    public T CreateMediumStrictlyPositive()
    {
        return CreatePositive(small, medium);
    }

    /// <inheritdoc />
    public T CreateLargeStrictlyPositive()
    {
        return CreatePositive(medium, large);
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
}
