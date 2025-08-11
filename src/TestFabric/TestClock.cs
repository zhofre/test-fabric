namespace TestFabric;

public class TestClock
{
    public DateTimeOffset Now { get; private set; } = DateTimeOffset.Now;

    public void StartAt(DateTimeOffset now)
    {
        Now = now;
    }

    public void Advance(TimeSpan timeSpan)
    {
        Now += timeSpan;
    }
}
