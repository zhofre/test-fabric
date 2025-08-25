namespace TestFabric.Test.Data;

[TestSubject(typeof(ObjectBuilder<>))]
public class ObjectBuilderTests
{
    private static ObjectBuilder<T> CreateSut<T>()
    {
        var fixture = new Fixture();
        return new ObjectBuilder<T>(fixture.Build<T>());
    }

    [Fact]
    public void Given_Name_When_WithName_Then_PersonHasName()
    {
        // Arrange
        const string name = "Hello";
        var sut = CreateSut<Person>();

        // Act
        var actual = sut
            .With(x => x.Name, name)
            .Create();

        // Assert
        actual.Name.Should().Be(name);
    }

    [Fact]
    public void Given_Nothing_When_WithoutName_Then_PersonHasNoName()
    {
        // Arrange
        var sut = CreateSut<Person>();

        // Act
        var actual = sut
            .Without(x => x.Name)
            .Create();

        // Assert
        actual.Name.Should().BeNull();
    }

    [Fact]
    public void Given_Name_When_WithNameAndCreateMany_Then_AllPersonsHaveName()
    {
        // Arrange
        const string name = "Hello";
        var sut = CreateSut<Person>();

        // Act
        var actual = sut
            .With(x => x.Name, name)
            .CreateMany();

        // Assert
        actual.Should().OnlyContain(person => person.Name == name);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Person
    {
        public string? Name { get; set; }
    }
}
