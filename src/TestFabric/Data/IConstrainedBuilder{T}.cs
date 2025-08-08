namespace TestFabric.Data;

/// <summary>
///     Interface for building instances with constraints.
///     Extends the basic IBuilder to allow adding options and ranges to control the generated objects.
/// </summary>
/// <typeparam name="T">Type of object to build.</typeparam>
public interface IConstrainedBuilder<T> : IBuilder<T>
{
    /// <summary>
    ///     Adds options to choose from.
    /// </summary>
    /// <param name="options">Array of options to add.</param>
    /// <returns>The reconfigured builder.</returns>
    IConstrainedBuilder<T> AddOptions(params T[] options);

    /// <summary>
    ///     Adds a range of options to choose from.
    /// </summary>
    /// <param name="range">range to consider</param>
    /// <returns>the reconfigured builder</returns>
    IConstrainedBuilder<T> AddRange<TRange>(TRange range) where TRange : IRange<T>;
}
