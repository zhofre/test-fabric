using Sample.Library.TestTools;
using TestFabric;

namespace Sample.Library.xUnit.Test;

public class PersonFactoryTests : TestSuite.Normal
{
    [Fact]
    public void Given_IdAndName_When_Create_Then_IdAndNameAsProperty()
    {
        // Arrange
        var id = Factory.Create<Guid>();
        var name = Factory.Create<string>();
        var age = Factory.CreateFromRange(16, 82);
        var guidGeneratorMock = new GuidGeneratorMockBuilder()
            .WithGenerate(id)
            .Create();
        var sut = new PersonFactory(guidGeneratorMock.Object);

        // Act
        var actual = sut.Create(name, age);

        // Assert
        Assert.Equal(id, actual.Id);
        Assert.Equal(name, actual.Name);
        Assert.Equal(age, actual.Age);
    }
}
