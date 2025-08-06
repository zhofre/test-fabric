using TestFabric;

namespace Sample.Library.TestTools;

public class GuidGeneratorMockBuilder : MockBuilder<IGuidGenerator>
{
    public GuidGeneratorMockBuilder WithGenerate(Guid value)
    {
        WithFunction(x => x.Generate(), value);
        return this;
    }
}
