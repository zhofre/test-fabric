using Sample.Library.TestTools;

namespace Sample.Library.MSTest.Test;

[TestClass]
public sealed class PersonFactoryTests
{
    [TestMethod]
    public void Given_IdAndName_When_Create_Then_IdAndNameAsProperty()
    {
        // Arrange
        var id = Guid.NewGuid();
        const string name = "John";
        var guidGeneratorMock = new GuidGeneratorMockBuilder()
            .WithGenerate(id)
            .Create();
        var sut = new PersonFactory(guidGeneratorMock.Object);

        // Act
        var actual = sut.Create(name);

        // Assert
        Assert.AreEqual(id, actual.Id);
        Assert.AreEqual(name, actual.Name);
    }
}
