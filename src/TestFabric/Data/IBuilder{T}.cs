using System.Linq.Expressions;

namespace TestFabric.Data;

/// <summary>
///     Interface for building instances of a specific type T.
///     Provides methods to configure and generate objects.
/// </summary>
/// <typeparam name="T">The type of object to build.</typeparam>
public interface IBuilder<T>
{
    /// <summary>
    ///     Overrides the value generated for a property.
    /// </summary>
    /// <typeparam name="TProperty">property type</typeparam>
    /// <param name="propertyPicker">expression to get the property</param>
    /// <param name="value">value to use</param>
    /// <returns>the reconfigured builder</returns>
    IBuilder<T> With<TProperty>(
        Expression<Func<T, TProperty>> propertyPicker,
        TProperty value);

    /// <summary>
    ///     Ignores a property.
    /// </summary>
    /// <typeparam name="TProperty">property type</typeparam>
    /// <param name="propertyPicker">expression to get the property</param>
    /// <returns>the reconfigured builder</returns>
    IBuilder<T> Without<TProperty>(
        Expression<Func<T, TProperty>> propertyPicker);

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
