namespace TestFabric;

/// <summary>
///     Simulates a clock for testing purposes, allowing control over the current time.
/// </summary>
public class TestClock
{
    /// <summary>
    ///     Represents the current date and time.
    /// </summary>
    public DateTimeOffset UtcNow { get; private set; } = DateTimeOffset.UtcNow;

    /// <summary>
    ///     Starts the clock at a specific point in time.
    /// </summary>
    /// <param name="utcNow">The DateTimeOffset representing the desired start time for the clock.</param>
    public void StartAt(DateTimeOffset utcNow)
    {
        UtcNow = utcNow;
    }

    /// <summary>
    ///     Advances the clock by a specified time span.
    /// </summary>
    /// <param name="timeSpan">The TimeSpan representing the amount of time to advance the clock.</param>
    public void Advance(TimeSpan timeSpan)
    {
        UtcNow += timeSpan;
    }
}
