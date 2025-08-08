using AutoFixture;
using TestFabric.Data;

// ReSharper disable MemberCanBePrivate.Global

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

    /// <summary>
    ///     Registers number factories (RandomDoubleFactory and RandomIntFactory) with the provided fixture.
    /// </summary>
    /// <param name="fixture">The fixture to register the factories with.</param>
    /// <param name="doubleFactory">
    ///     Optional function to customize double factory creation. If null, the default behavior is
    ///     used.
    /// </param>
    /// <param name="intFactory">
    ///     Optional function to customize integer factory creation. If null, the default behavior is
    ///     used.
    /// </param>
    /// <returns>The same fixture instance, enabling method chaining.</returns>
    public static Fixture RegisterNumberFactories(
        this Fixture fixture,
        Func<RandomDoubleFactory, Func<double>> doubleFactory = null,
        Func<RandomIntFactory, Func<int>> intFactory = null)
    {
        var i = new RandomIntFactory();
        var d = new RandomDoubleFactory();
        return fixture
            .RegisterSingleton(d)
            .RegisterSingleton(i)
            .SetDoubleGenerator(doubleFactory?.Invoke(d) ?? d.Create)
            .SetIntGenerator(intFactory?.Invoke(i) ?? i.Create);
    }

    /// <summary>
    ///     Registers a singleton instance with the provided fixture.
    /// </summary>
    /// <param name="fixture">The fixture to register the singleton with.</param>
    /// <param name="instance">The instance to register as a singleton.</param>
    /// <returns>The same fixture instance, enabling method chaining.</returns>
    public static Fixture RegisterSingleton<T>(this Fixture fixture, T instance)
    {
        fixture.Register(() => instance);
        return fixture;
    }

    /// <summary>
    ///     Sets a custom generator for double values in the fixture.
    /// </summary>
    /// <param name="fixture">The fixture to configure.</param>
    /// <param name="generator">A function that returns a random double value.</param>
    /// <returns>The same fixture instance, enabling method chaining.</returns>
    public static Fixture SetDoubleGenerator(this Fixture fixture, Func<double> generator)
    {
        fixture.Customize<double>(ob => ob.FromFactory(generator));
        return fixture;
    }

    /// <summary>
    ///     Sets the integer generator for the provided fixture.
    /// </summary>
    /// <param name="fixture">The fixture to set the integer generator for.</param>
    /// <param name="generator">The function to generate integers. If null, the default behavior is used.</param>
    /// <returns>The same fixture instance, enabling method chaining.</returns>
    public static Fixture SetIntGenerator(this Fixture fixture, Func<int> generator)
    {
        fixture.Customize<int>(ob => ob.FromFactory(generator));
        return fixture;
    }

    /// <summary>
    ///     Allows recursion within the fixture.
    /// </summary>
    /// <param name="fixture">The fixture to allow recursion for.</param>
    /// <returns>The same fixture instance, enabling method chaining.</returns>
    public static Fixture AllowRecursion(this Fixture fixture)
    {
        fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        return fixture;
    }
}
