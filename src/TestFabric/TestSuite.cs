using TestFabric.Data;

namespace TestFabric;

/// <summary>
///     Host type for test suites that use a factory builder to create data instances.
/// </summary>
public static class TestSuite
{
    /// <summary>
    ///     Base class for test suites that use a normal factory builder to create data instances.
    /// </summary>
    public abstract class Normal : TestSuite<FactoryBuilders.Normal>;

    /// <summary>
    ///     Base class for test suites that use a factory builder to create data instances with recursion enabled.
    /// </summary>
    public abstract class WithRecursion : TestSuite<FactoryBuilders.WithRecursion>;
}
