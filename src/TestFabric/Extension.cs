using TestFabric.Data;

namespace TestFabric;

/// <summary>
///     Provides a set of extension methods.
/// </summary>
public static class Extension
{
    /// <summary>
    ///     Converts an integer seed to a Random object.
    ///     If the seed is null, it uses the default Random constructor.
    /// </summary>
    /// <param name="seed">An optional integer seed for the random number generator.</param>
    /// <returns>A new instance of Random using the provided seed or the default if no seed is specified.</returns>
    internal static Random ToRandom(this int? seed)
    {
        return seed.HasValue
            ? new Random(seed.Value)
            : new Random();
    }

    /// <summary>
    ///     Adds a range of options to choose from.
    /// </summary>
    /// <param name="builder">The builder being configured.</param>
    /// <param name="start">The starting value of the range.</param>
    /// <param name="end">The ending value of the range (exclusive).</param>
    /// <returns>The reconfigured builder.</returns>
    public static IConstrainedBuilder<T> AddRange<T>(this IConstrainedBuilder<T> builder, T start, T end)
        where T : IComparable<T>
    {
        return builder.AddRange(new NumberRange<T>(start, end));
    }
}
