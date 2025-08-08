using System.Globalization;
using TestFabric.Data;

namespace TestFabric;

public class TestSuite<TDataFactoryBuilder> where TDataFactoryBuilder : IFactoryBuilder, new()
{
    // ReSharper disable once StaticMemberInGenericType
    // it is intended that there is a different instance per type of test class
    protected static readonly IFactory Factory;

    static TestSuite()
    {
        Factory = new TDataFactoryBuilder()
            .Create();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    ///     Sets the current culture for both CurrentCulture and CurrentUICulture.
    /// </summary>
    /// <param name="info">The CultureInfo to set as the current culture.</param>
    protected void SetCurrentCulture(CultureInfo info)
    {
        Thread.CurrentThread.CurrentCulture = info;
        Thread.CurrentThread.CurrentUICulture = info;
    }

    /// <summary>
    ///     Sets the current culture for both CurrentCulture and CurrentUICulture.
    /// </summary>
    /// <param name="culture">The name of the culture info to set as the current culture.</param>
    protected void SetCurrentCulture(string culture)
    {
        SetCurrentCulture(CultureInfo.CreateSpecificCulture(culture));
    }

    /// <summary>
    ///     Sets the current culture for both CurrentCulture and CurrentUICulture to Invariant.
    /// </summary>
    protected void SetCurrentCultureInvariant()
    {
        SetCurrentCulture(CultureInfo.InvariantCulture);
    }

    /// <summary>
    ///     Creates an anonymous object.
    /// </summary>
    /// <typeparam name="T">type to create</typeparam>
    /// <returns>anonymous object</returns>
    protected T Random<T>()
    {
        return Factory.Create<T>();
    }

    /// <summary>
    ///     Creates an anonymous object from a range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="minInclusive">minimum value in range</param>
    /// <param name="maxExclusive">maximum value not in range</param>
    /// <returns>object in the provided range</returns>
    protected T InRange<T>(T minInclusive, T maxExclusive) where T : IComparable<T>
    {
        return Factory.CreateFromRange(minInclusive, maxExclusive);
    }

    /// <summary>
    ///     Creates an anonymous object from a range.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">values in range</param>
    /// <returns>object from the provided items</returns>
    protected T InRange<T>(IEnumerable<T> items)
    {
        return Factory.CreateFromRange(items);
    }
}
