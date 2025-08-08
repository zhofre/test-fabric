using JetBrains.Annotations;
using TestFabric.Data;

namespace TestFabric.Test.Data;

[TestSubject(typeof(Builder<>))]
public class BuilderTests
{
    public static Builder<T> CreateSut<T>()
    {
        var fixture = new Fixture();
        return new Builder<T>(fixture.Build<T>());
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
        Assert.Equal(name, actual.Name);
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
        Assert.Null(actual.Name);
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
        foreach (var person in actual)
        {
            Assert.Equal(name, person.Name);
        }
    }

    public class Person
    {
        public string? Name { get; set; }
    }
}
