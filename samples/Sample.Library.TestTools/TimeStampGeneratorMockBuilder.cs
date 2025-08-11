using TestFabric;

namespace Sample.Library.TestTools;

public class TimeStampGeneratorMockBuilder : MockBuilder<ITimeStampGenerator>
{
    public TimeStampGeneratorMockBuilder WithGenerate(DateTimeOffset value)
    {
        WithFunction(x => x.Generate(), value);
        return this;
    }

    public TimeStampGeneratorMockBuilder WithGenerate(TestClock clock)
    {
        WithFunction(
            x => x.Generate(),
            () => clock.UtcNow);
        return this;
    }
}
