using JetBrains.Annotations;
using TestFabric.Data;

// ReSharper disable MemberCanBePrivate.Global

namespace TestFabric.Test.Data;

[TestSubject(typeof(ConstrainedBuilder<>))]
public class ConstrainedBuilderTests
{
    private static ConstrainedBuilder<int> CreateSut()
    {
        return new ConstrainedBuilder<int>(new IntPicker());
    }

    private static ConstrainedBuilder<Person> CreatePersonSut()
    {
        return new ConstrainedBuilder<Person>(null);
    }

    [Fact]
    public void Given_Nothing_When_With_Then_InvalidOperation()
    {
        // Arrange
        var sut = CreatePersonSut();

        // Act+Assert
        Assert.Throws<InvalidOperationException>(() => sut.With(x => x.Name, "name"));
    }


    [Fact]
    public void Given_Nothing_When_Without_Then_InvalidOperation()
    {
        // Arrange
        var sut = CreatePersonSut();

        // Act+Assert
        Assert.Throws<InvalidOperationException>(() => sut.Without(x => x.Name));
    }

    [Fact]
    public void Given_Nothing_When_Create_Then_InvalidOperation()
    {
        // Arrange
        var sut = CreateSut();

        // Act+Assert
        Assert.Throws<InvalidOperationException>(() => sut.Create());
    }

    [Fact]
    public void Given_Options_When_Create_Then_FromOptions()
    {
        // Arrange
        var options = new[] { 7, 2, 12 };
        var sut = CreateSut();

        // Act
        var actual = sut
            .AddOptions(options)
            .Create();

        // Assert
        Assert.Contains(actual, options);
    }

    [Fact]
    public void Given_Options_When_CreateMany_Then_FromOptions()
    {
        // Arrange
        var options = new[] { 7, 2, 12 };
        var sut = CreateSut();

        // Act
        var actual = sut
            .AddOptions(options)
            .CreateMany();

        // Assert
        foreach (var item in actual)
        {
            Assert.Contains(item, options);
        }
    }

    [Fact]
    public void Given_NullOptions_When_Create_Then_InvalidOperation()
    {
        // Arrange
        var sut = CreateSut();

        // Act+Assert
        Assert.Throws<InvalidOperationException>(() => sut.AddOptions(null).Create());
    }

    [Fact]
    public void Given_NullRange_When_Create_Then_InvalidOperation()
    {
        // Arrange
        var sut = CreateSut();

        // Act+Assert
        Assert.Throws<InvalidOperationException>(() => sut.AddRange<IRange<int>>(null!).Create());
    }

    [Fact]
    public void Given_StartEnd_When_AddRangeExtension_Then_InRange()
    {
        // Arrange
        const int start = 3;
        const int end = 20;

        var sut = CreateSut();

        // Act
        var actual = sut.AddRange(start, end);
        var value = actual.Create();

        // Assert
        Assert.True(start <= value);
        Assert.True(end > value);
    }

    [Fact]
    public void Given_Person_When_SetAndGet_Then_Same()
    {
        // Arrange
        var sut = new Person();
        const string name = "John";

        // Act
        sut.Name = name;
        var actual = sut.Name;

        // Assert
        Assert.Equal(name, actual);
    }

    public class Person
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string? Name { get; set; }
    }
}
