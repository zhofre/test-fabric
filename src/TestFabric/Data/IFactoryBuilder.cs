namespace TestFabric.Data;

/// <summary>
///     Interface used by test suites to create a data factory.
/// </summary>
public interface IFactoryBuilder
{
    /// <summary>
    ///     Creates the data factory.
    /// </summary>
    /// <returns></returns>
    IFactory Create();
}
