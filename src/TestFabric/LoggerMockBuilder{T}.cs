using Microsoft.Extensions.Logging;

namespace TestFabric;

public class LoggerMockBuilder<T> : MockBuilder<ILogger<T>>
{
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
