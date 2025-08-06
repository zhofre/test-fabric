namespace TestFabric;

/// <summary>
///     Builder for creating mock objects of type <see cref="IProgress{T}" /> with fluent methods.
/// </summary>
/// <typeparam name="T">Type parameter for the progress report.</typeparam>
public class ProgressMockBuilder<T> : MockBuilder<IProgress<T>>
{
    /// <summary>
    ///     Configures the mock to execute a default action when the Report method is called.
    /// </summary>
    /// <returns>The current builder instance for fluent chaining.</returns>
    public ProgressMockBuilder<T> WithReport()
    {
        WithAction(x => x.Report(It.IsAny<T>()));
        return this;
    }

    /// <summary>
    ///     Configures the mock to execute a specific action when the Report method is called.
    /// </summary>
    /// <param name="callback">The action to be executed when the Report method is called.</param>
    /// <returns>The current builder instance for fluent chaining.</returns>
    public ProgressMockBuilder<T> WithReport(Action<T> callback)
    {
        WithAction(x => x.Report(It.IsAny<T>()), callback);
        return this;
    }
}
