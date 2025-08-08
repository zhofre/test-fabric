using System.Globalization;
using TestFabric.Data;

namespace TestFabric;

public class TestSuite<TDataFactoryBuilder> where TDataFactoryBuilder : IFactoryBuilder, new()
{
    // it is intended that there is a different instance per type of test class
    // ReSharper disable once StaticMemberInGenericType
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
}
