using Microsoft.Extensions.Logging;

namespace TestFabric;

/// <summary>
///     Builder for creating a mock implementation of <see cref="ILogger{T}" />.
/// </summary>
/// <typeparam name="T">The type used as a category name by the logger.</typeparam>
public class LoggerMockBuilder<T> : MockBuilder<ILogger<T>>
{
    /// <summary>
    ///     Configures the mock to capture any log message.
    /// </summary>
    /// <returns>The current <see cref="LoggerMockBuilder{T}" /> instance, allowing for method chaining.</returns>
    public LoggerMockBuilder<T> WithLog()
    {
        WithAction(x => x.Log(
            It.IsAny<LogLevel>(),
            It.IsAny<EventId>(),
            It.IsAny<It.IsAnyType>(),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()));
        return this;
    }

    /// <summary>
    ///     Configures the mock to capture any log message in the provided list.
    /// </summary>
    /// <param name="log">The list to add log messages to.</param>
    /// <returns>The current <see cref="LoggerMockBuilder{T}" /> instance, allowing for method chaining.</returns>
    public LoggerMockBuilder<T> WithLog(List<string> log)
    {
        return WithLog((level, text, ex) =>
        {
            text = ex != null
                ? $"{text} | {ex.Message}"
                : text;
            log.Add($"[{level}] {text}");
        });
    }

    /// <summary>
    ///     Configures the mock to capture any log message.
    /// </summary>
    /// <param name="handler">The action to handle the log message.</param>
    /// <returns>The current <see cref="LoggerMockBuilder{T}" /> instance, allowing for method chaining.</returns>
    public LoggerMockBuilder<T> WithLog(Action<LogLevel, string, Exception> handler)
    {
        Mock
            .Setup(x => x.Log(
                It.IsAny<LogLevel>(),
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()))
            .Callback(
                new InvocationAction(invocation =>
                {
                    var level = (LogLevel)invocation.Arguments[0];
                    //var eventId = (EventId)invocation.Arguments[1];
                    var state = invocation.Arguments[2];
                    var exception = (Exception)invocation.Arguments[3];
                    var formatter = invocation.Arguments[4];

                    var invokeMethod = formatter.GetType().GetMethod("Invoke");
                    var text = (string)invokeMethod?.Invoke(formatter, new[] { state, exception });
                    handler(level, text, exception);
                }));

        return this;
    }
}
