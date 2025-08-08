namespace TestFabric.Data;

/// <summary>
///     Base interface for building instances of a specific type T.
///     Provides methods to generate objects.
/// </summary>
/// <typeparam name="T">The type of object to build.</typeparam>
public interface IBuilder<out T>
{
    /// <summary>
    ///     Creates the instance.
    /// </summary>
    /// <returns>the instance</returns>
    T Create();

    /// <summary>
    ///     Creates many anonymous objects.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <returns>anonymous objects</returns>
    IEnumerable<T> CreateMany();

    /// <summary>
    ///     Creates many anonymous objects.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <param name="count">number of objects to create</param>
    /// <returns>anonymous objects</returns>
    IEnumerable<T> CreateMany(int count);

    /// <summary>
    ///     Creates many constrained anonymous objects.
    /// </summary>
    /// <param name="countMin">min number of objects to create (inclusive)</param>
    /// <param name="countMax">max number of objects to create (inclusive)</param>
    /// <returns>anonymous objects</returns>
    IEnumerable<T> CreateMany(int countMin, int countMax);
}
