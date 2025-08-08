using System.Linq.Expressions;

namespace TestFabric.Data;

/// <summary>
///     Interface for building instances of a specific type T.
///     Provides methods to configure and generate objects.
/// </summary>
/// <typeparam name="T">The type of object to build.</typeparam>
public interface IObjectBuilder<T> : IBuilder<T>
{
    /// <summary>
    ///     Overrides the value generated for a property.
    /// </summary>
    /// <typeparam name="TProperty">property type</typeparam>
    /// <param name="propertyPicker">expression to get the property</param>
    /// <param name="value">value to use</param>
    /// <returns>the reconfigured builder</returns>
    IObjectBuilder<T> With<TProperty>(
        Expression<Func<T, TProperty>> propertyPicker,
        TProperty value);

    /// <summary>
    ///     Ignores a property.
    /// </summary>
    /// <typeparam name="TProperty">property type</typeparam>
    /// <param name="propertyPicker">expression to get the property</param>
    /// <returns>the reconfigured builder</returns>
    IObjectBuilder<T> Without<TProperty>(
        Expression<Func<T, TProperty>> propertyPicker);
}
