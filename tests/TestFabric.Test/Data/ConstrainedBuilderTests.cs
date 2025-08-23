// ReSharper disable MemberCanBePrivate.Global

namespace TestFabric.Test.Data;

[TestSubject(typeof(ConstrainedBuilder<>))]
public class ConstrainedBuilderTests
{
    private static ConstrainedBuilder<int> CreateSut()
    {
        return new ConstrainedBuilder<int>(new IntPicker());
    }

    [Fact]
    public void Given_Nothing_When_Create_Then_InvalidOperation()
    {
        // Arrange
        var sut = CreateSut();

        // Act + Assert
        Action act = () => sut.Create();
        act.Should().Throw<InvalidOperationException>();
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
        options.Should().Contain(actual);
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
            options.Should().Contain(item);
        }
    }

    [Fact]
    public void Given_NullOptions_When_Create_Then_InvalidOperation()
    {
        // Arrange
        var sut = CreateSut();

        // Act + Assert
        Action act = () => sut.AddOptions(null!).Create();
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void Given_NullRange_When_Create_Then_InvalidOperation()
    {
        // Arrange
        var sut = CreateSut();

        // Act + Assert
        Action act = () => sut.AddRange<IRange<int>>(null!).Create();
        act.Should().Throw<InvalidOperationException>();
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
        value.Should().BeGreaterThanOrEqualTo(start);
        value.Should().BeLessThan(end);
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
        actual.Should().Be(name);
    }

    public class Person
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string? Name { get; set; }
    }
}
