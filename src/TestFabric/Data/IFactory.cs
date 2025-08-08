namespace TestFabric.Data;

public interface IFactory
{
    /// <summary>
    ///     Creates an anonymous object.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <returns>anonymous objects</returns>
    T Create<T>();

    /// <summary>
    ///     Creates many anonymous objects.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <returns>anonymous objects</returns>
    IEnumerable<T> CreateMany<T>();

    /// <summary>
    ///     Creates many anonymous objects.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <param name="count">number of objects to create</param>
    /// <returns>anonymous objects</returns>
    IEnumerable<T> CreateMany<T>(int count);

    /// <summary>
    ///     Creates many anonymous objects.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <param name="countMin">min number of objects to create (inclusive)</param>
    /// <param name="countMax">max number of objects to create (inclusive)</param>
    /// <returns>anonymous objects</returns>
    IEnumerable<T> CreateMany<T>(int countMin, int countMax);

    /// <summary>
    ///     Creates an anonymous object from a range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="minInclusive">minimum value in range</param>
    /// <param name="maxExclusive">maximum value not in range</param>
    /// <returns></returns>
    T CreateFromRange<T>(
        T minInclusive,
        T maxExclusive)
        where T : IComparable<T>;

    /// <summary>
    ///     Creates many anonymous objects from a range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="minInclusive">minimum value in range</param>
    /// <param name="maxExclusive">maximum value not in range</param>
    /// <returns></returns>
    IEnumerable<T> CreateManyFromRange<T>(
        T minInclusive,
        T maxExclusive)
        where T : IComparable<T>;


    /// <summary>
    ///     Creates many anonymous objects from a range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="count">number of objects to create</param>
    /// <param name="minInclusive">minimum value in range</param>
    /// <param name="maxExclusive">maximum value not in range</param>
    /// <returns></returns>
    IEnumerable<T> CreateManyFromRange<T>(
        int count,
        T minInclusive,
        T maxExclusive)
        where T : IComparable<T>;

    /// <summary>
    ///     Creates an anonymous object from a range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">values in range</param>
    /// <returns></returns>
    T CreateFromRange<T>(IEnumerable<T> items);

    /// <summary>
    ///     Creates many anonymous objects from a range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">values in range</param>
    /// <returns></returns>
    IEnumerable<T> CreateManyFromRange<T>(IEnumerable<T> items);


    /// <summary>
    ///     Creates many anonymous objects from a range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="count">number of objects to create</param>
    /// <param name="items">values in range</param>
    /// <returns></returns>
    IEnumerable<T> CreateManyFromRange<T>(
        int count,
        IEnumerable<T> items);

    /// <summary>
    ///     Creates a builder to customize the data creation.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <returns>a data builder</returns>
    IBuilder<T> Build<T>();

    /// <summary>
    ///     Creates a builder for constrained data.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <returns>a constrained data builder</returns>
    IConstrainedBuilder<T> BuildConstrained<T>();

    /// <summary>
    ///     Creates a builder for constrained data from a single range.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <param name="minInclusive">minimum value in range</param>
    /// <param name="maxExclusive">maximum value not in range</param>
    /// <returns>a constrained data builder</returns>
    IConstrainedBuilder<T> BuildConstrainedFromRange<T>(
        T minInclusive,
        T maxExclusive)
        where T : IComparable<T>;

    /// <summary>
    ///     Creates a builder for constrained data from a single range.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <param name="items">values in range</param>
    /// <returns>a constrained data builder</returns>
    IConstrainedBuilder<T> BuildConstrainedFromRange<T>(IEnumerable<T> items);
}
