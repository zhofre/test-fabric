using JetBrains.Annotations;
using TestFabric.Data;

namespace TestFabric.Test.Data;

[TestSubject(typeof(Factory))]
public class FactoryTests
{
    private static Factory CreateSut(params IPicker[] pickers)
    {
        return new Factory(
            new Fixture(),
            null,
            pickers);
    }

    [Fact]
    public void Given_Nothing_When_CreatePerson_Then_HasName()
    {
        // Arrange
        var sut = CreateSut();

        // Act
        var actual = sut.Create<Person>();

        // Assert
        Assert.NotNull(actual.Name);
    }

    [Fact]
    public void Given_Nothing_When_CreateManyPerson_Then_Multiple()
    {
        // Arrange
        var sut = CreateSut();

        // Act
        var actual = sut.CreateMany<Person>();

        // Assert
        Assert.NotEmpty(actual);
    }

    [Fact]
    public void Given_Count_When_CreateManyPerson_Then_ExactCount()
    {
        // Arrange
        const int count = 7;
        var sut = CreateSut();

        // Act
        var actual = sut.CreateMany<Person>(count);

        // Assert
        Assert.Equal(count, actual.Count());
    }

    [Fact]
    public void Given_MinAndMaxCount_When_CreateManyPerson_Then_InclusiveBetweenCount()
    {
        // Arrange
        const int minCount = 7;
        const int maxCount = 10;
        var sut = CreateSut();

        // Act
        var actual = sut.CreateMany<Person>(minCount, maxCount);
        var actualCount = actual.Count();

        // Assert
        Assert.True(actualCount >= minCount);
        Assert.True(actualCount <= maxCount);
    }

    [Fact]
    public void Given_Nothing_When_CreateErrorPerson_Then_FactoryException()
    {
        // Arrange
        var sut = CreateSut();

        // Act+Assert
        Assert.Throws<FactoryException>(sut.Create<ErrorPerson>);
    }

    [Fact]
    public void Given_Nothing_When_CreateManyErrorPerson_Then_FactoryException()
    {
        // Arrange
        var sut = CreateSut();

        // Act+Assert
        Assert.Throws<FactoryException>(sut.CreateMany<ErrorPerson>);
    }

    [Fact]
    public void Given_IntRange_When_CreateFromRange_Then_NoPickerException()
    {
        // Arrange
        const int minIncl = 3;
        const int maxExcl = 10;
        var sut = CreateSut();

        // Act+Assert
        Assert.Throws<InvalidOperationException>(() => sut.CreateFromRange(minIncl, maxExcl));
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_CreateFromRange_Then_IntFromRange()
    {
        // Arrange
        const int minIncl = 3;
        const int maxExcl = 10;
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateFromRange(minIncl, maxExcl);

        // Assert
        Assert.True(minIncl <= actual);
        Assert.True(actual < maxExcl);
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_CreateManyFromRange_Then_IntFromRange()
    {
        // Arrange
        const int minIncl = 3;
        const int maxExcl = 10;
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateManyFromRange(minIncl, maxExcl);

        // Assert
        foreach (var item in actual)
        {
            Assert.True(minIncl <= item);
            Assert.True(item < maxExcl);
        }
    }

    [Fact]
    public void Given_IntRangeAndPickerAndCount_When_CreateManyFromRange_Then_IntFromRange()
    {
        // Arrange
        const int minIncl = 3;
        const int maxExcl = 10;
        const int count = 9;
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateManyFromRange(count, minIncl, maxExcl);

        // Assert
        Assert.Equal(count, actual.Count());
        foreach (var item in actual)
        {
            Assert.True(minIncl <= item);
            Assert.True(item < maxExcl);
        }
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_CreateFromItems_Then_IntFromRange()
    {
        // Arrange
        int[] items = [3, 4, 6, 9];
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateFromRange(items);

        // Assert
        Assert.Contains(actual, items);
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_CreateManyFromItems_Then_IntFromRange()
    {
        // Arrange
        int[] items = [3, 4, 6, 9];
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateManyFromRange(items);

        // Assert
        foreach (var item in actual)
        {
            Assert.Contains(item, items);
        }
    }

    [Fact]
    public void Given_IntRangeAndPickerAndCount_When_CreateManyFromItems_Then_IntFromRange()
    {
        // Arrange
        int[] items = [3, 4, 6, 9];
        const int count = 9;
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.CreateManyFromRange(count, items);

        // Assert
        Assert.Equal(count, actual.Count());
        foreach (var item in actual)
        {
            Assert.Contains(item, items);
        }
    }

    [Fact]
    public void Given_Nothing_When_BuildPerson_Then_BuilderReturned()
    {
        // Arrange
        var sut = CreateSut();

        // Act
        var actual = sut.Build<Person>();

        // Assert
        Assert.IsType<Builder<Person>>(actual);
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_BuildConstrainedFromRange_Then_ConstrainedBuilderOfInt()
    {
        // Arrange
        const int minIncl = 3;
        const int maxExcl = 10;
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.BuildConstrainedFromRange(minIncl, maxExcl);

        // Assert
        Assert.IsType<ConstrainedBuilder<int>>(actual);
    }

    [Fact]
    public void Given_IntRangeAndPicker_When_BuildConstrainedFromItems_Then_ConstrainedBuilderOfInt()
    {
        // Arrange
        int[] items = [3, 4, 6, 9];
        var sut = CreateSut(
            new IntPicker());

        // Act
        var actual = sut.BuildConstrainedFromRange(items);

        // Assert
        Assert.IsType<ConstrainedBuilder<int>>(actual);
    }

    public class Person
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        public string? Name { get; set; }
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public class ErrorPerson : Person
    {
        public ErrorPerson()
        {
            throw new Exception();
        }
    }
}
