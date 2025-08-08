namespace TestFabric.Data;

/// <summary>
///     Defines a factory interface for creating different types of random numbers.
/// </summary>
public interface IRandomNumberFactory<out T>
{
    /// <summary>
    ///     Creates a random number of the specified type.
    /// </summary>
    /// <returns>A random number of type T.</returns>
    T Create();

    /// <summary>
    ///     Creates a small random number of the specified type.
    /// </summary>
    /// <returns>A small random number of type T.</returns>
    T CreateSmall();

    /// <summary>
    ///     Creates a large random number of the specified type.
    /// </summary>
    /// <returns>A large random number of type T.</returns>
    T CreateLarge();

    /// <summary>
    ///     Creates a positive random number of the specified type.
    /// </summary>
    /// <returns>A positive random number of type T.</returns>
    T CreatePositive();

    /// <summary>
    ///     Creates a small positive random number of the specified type.
    /// </summary>
    /// <returns>A small positive random number of type T.</returns>
    T CreateSmallPositive();

    /// <summary>
    ///     Creates a large positive random number of the specified type.
    /// </summary>
    /// <returns>A large positive random number of type T.</returns>
    T CreateLargePositive();

    /// <summary>
    ///     Creates a strictly positive random number of the specified type.
    /// </summary>
    /// <returns>A strictly positive random number of type T.</returns>
    T CreateStrictlyPositive();

    /// <summary>
    ///     Creates a small and strictly positive random number of the specified type.
    /// </summary>
    /// <returns>A small and strictly positive random number of type T.</returns>
    T CreateSmallStrictlyPositive();

    /// <summary>
    ///     Creates a large and strictly positive random number of the specified type.
    /// </summary>
    /// <returns>A large and strictly positive random number of type T.</returns>
    T CreateLargeStrictlyPositive();
}
