using TestFabric;

namespace Sample.Library.xUnit.Test;

public class PersonTests : TestSuite.Normal
{
    [Fact]
    public void Given_Nothing_When_Random_Then_PersonWithData()
    {
        // Arrange

        // Act
        var person = Random<Person>();

        // Assert
        person.Should().NotBeNull();
        person.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public void Given_Id_When_BuildWithId_Then_PersonWithId()
    {
        // Arrange
        var id = Random<Guid>();

        // Act
        var person = Factory.Build<Person>()
            .With(x => x.Id, id)
            .Create();

        // Assert
        person.Id.Should().Be(id);
    }
}
